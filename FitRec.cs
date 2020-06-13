using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;


namespace Logger
{
    public class FitRec : App
    {
        private string prectype;
        private string pfitnum;


        public bool ValidateFit(string fitNum)
        {
            string sql = @"SELECT * FROM [screeninfo] " +
                                 "WHERE [scrnum ] ='" + fitNum + "'";

            Dictionary<string, FitRec> resultData = readData(sql);

            if (resultData.Count <= 0)
            {
                Console.WriteLine("Error FIT number does not exist");
                return false;
            }
            return true;

        }

        public new Dictionary<string, FitRec> readData(string sql)
        {
            Utility U = new Utility();
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

                Dictionary<string, FitRec> dicData = new Dictionary<string, FitRec>();

                while (dataReader.Read())
                {
                    FitRec fr = new FitRec();
                    fr.prectype = dataReader.GetString(2);
                    fr.pfitnum = dataReader.GetString(3);
                    dicData.Add(dataReader.GetString(1) + dataReader.GetInt32(0).ToString(), fr);
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
                    if (r.typeContent.Length == 8)
                    {
                        continue;
                    }
                    if (r.typeContent.Substring(0, 3) == "000")
                    {
                        loadNum++;
                    }
                    // convert to hex
                    string[] fit2Hex = new string[42];

                    for (int x = 0, y = 0; x <= r.typeContent.Length - 1; x = x + 3, y++)
                    {
                        string s1 = r.typeContent.Substring(x, 3);
                        if (x == 0)
                        {
                            fit2Hex[y] = s1;
                            continue;
                        }
                        Utility U = new Utility();
                        fit2Hex[y] = U.dec2hex(s1, 2);
                    }

                    sql = @"INSERT INTO fitinfo([logkey],[rectype],[fitnum],[piddx],[pfiid],[pstdx],[pagdx],[pmxpn]," +
                                               "[pckln],[pinpd],[pandx],[panln],[panpd], [prcnt],[pofdx],[pdctb]," +
                                               "[pekey],[pindx],[plndx],[pmmsr],[reserved],[pbfmt],[load],[prjkey],[logID])" +
                          " VALUES('" + r.typeIndex + "','" + // key
                                       'F' + "','" + // record type
                                       fit2Hex[0] + "','" + // fitnum
                                       fit2Hex[1] + "','" + // piddx
                                       fit2Hex[2] + fit2Hex[3] + fit2Hex[4] +
                                       fit2Hex[5] + fit2Hex[6] + "','" + // pfiid
                                       fit2Hex[7] + "','" + // pstdx
                                       fit2Hex[8] + "','" + // pagdx
                                       fit2Hex[9] + "','" + // pmxpn
                                       fit2Hex[10] + "','" + // pckln
                                       fit2Hex[11] + "','" + // pinpd
                                       fit2Hex[12] + "','" + // pandx
                                       fit2Hex[13] + "','" + // panln
                                       fit2Hex[14] + "','" + // panpd
                                       fit2Hex[15] + "','" + // prcnt
                                       fit2Hex[16] + "','" + // pofdx
                                       fit2Hex[17] + fit2Hex[18] + fit2Hex[19] +
                                       fit2Hex[20] + fit2Hex[21] + fit2Hex[22] +
                                       fit2Hex[23] + fit2Hex[24] + "','" + // pdctb
                                       fit2Hex[25] + fit2Hex[26] + fit2Hex[27] +
                                       fit2Hex[28] + fit2Hex[29] + fit2Hex[30] +
                                       fit2Hex[31] + fit2Hex[32] + "','" + // pekey
                                       fit2Hex[33] + fit2Hex[34] + fit2Hex[35] + "','" + // pindx
                                       fit2Hex[36] + "','" + // plndx
                                       fit2Hex[37] + "','" + // pmmsr
                                       fit2Hex[38] + fit2Hex[39] + fit2Hex[40] + "','" + // reserved
                                       fit2Hex[41] + "','" + // pbfmt
                                       loadNum + "','" + // load
                                       Key + "'," + // project key
                                       logID + ")";


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

