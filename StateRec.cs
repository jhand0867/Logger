﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;


namespace Logger
{
    public class stateRec : App

    {
        private string pStateNum;
        private string pStateType;
        private string pSta1;
        private string pSta2;
        private string pSta3;
        private string pSta4;
        private string pSta5;
        private string pSta6;
        private string pSta7;
        private string pSta8;


        public string stateNum   // property
        => pStateNum;
        public string stateType   // property
        => pStateType;
        public string sta1   // property
        => pSta1;
        public string sta2   // property
        => pSta2;
        public string sta3   // property
        => pSta3;
        public string sta4   // property
        => pSta4;
        public string sta5   // property
        => pSta5;
        public string sta6   // property
        => pSta6;
        public string sta7   // property
        => pSta7;
        public string sta8   // property
        => pSta8;

        public virtual void ValidateState(stateRec stateData)
        {
            string stateTypes = @"ABCDEFGHIJKLMNORSTVWXYZbdefgkmwz->&z";
            if (!(stateTypes.Contains(stateData.stateType)))
            {
                Console.WriteLine("Not a valid state type");
            }
        }

        public Dictionary<string, stateRec> ValidateStateNumber(string value)
        {
            int stateNum = Convert.ToInt32(value);
            if (!(stateNum >= 0 && stateNum <= 999))
            {
                Console.WriteLine("Value not in range 000 to 999");
                return null;
            }

            string sql = @"SELECT * FROM [stateinfo] " +
                          "WHERE [stateNum ] ='" + value + "'";

            Dictionary<string, stateRec> resultData = this.readData(sql);

            if (resultData.Count <= 0)
            {
                Console.WriteLine("Error state does not exist");
                return null;
            }
            return resultData;
        }


        public new Dictionary<string, stateRec> readData(string sql)
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

                //sql = @"SELECT logkey,group8 from [logger].[dbo].[loginfo] WHERE group8 like 'HOST2ATM: 30%12%'";

                command = new SqlCommand(sql, cnn);
                // dataReader.GetOrdinal(0);

                dataReader = command.ExecuteReader();

                Dictionary<string, stateRec> dicData = new Dictionary<string, stateRec>();




                while (dataReader.Read())
                {

                    stateRec sr = new stateRec();
                    sr.pStateNum = dataReader.GetString(4);
                    sr.pStateType = dataReader.GetString(5);
                    sr.pSta1 = dataReader.GetString(6);
                    sr.pSta2 = dataReader.GetString(7);
                    sr.pSta3 = dataReader.GetString(8);
                    sr.pSta4 = dataReader.GetString(9);
                    sr.pSta5 = dataReader.GetString(10);
                    sr.pSta6 = dataReader.GetString(11);
                    sr.pSta7 = dataReader.GetString(12);
                    sr.pSta8 = dataReader.GetString(13);

                    dicData.Add(dataReader.GetString(2) + dataReader.GetInt64(0).ToString(), sr);
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
                    // MAC lenght
                    if (r.typeContent.Length == 8)
                    {
                        continue;
                    }
                    if (r.typeContent.Substring(0, 3) == "000" && r.typeContent.Substring(3, 1) == "A")
                    {
                        loadNum++;
                    }

                    sql = @"INSERT INTO stateinfo([prjkey],[logkey],[rectype],[statenum],[statetype]," +
                          "[sta1],[sta2],[sta3],[sta4],[sta5],[sta6],[sta7],[sta8],[load],[logID]) " +
                          " VALUES('" + Key + "','" +
                                        r.typeIndex + "','" +
                                       'S' + "','" +
                                       r.typeContent.Substring(0, 3) + "','" +
                                       r.typeContent.Substring(3, 1) + "','" +
                                       r.typeContent.Substring(4, 3) + "','" +
                                       r.typeContent.Substring(7, 3) + "','" +
                                       r.typeContent.Substring(10, 3) + "','" +
                                       r.typeContent.Substring(13, 3) + "','" +
                                       r.typeContent.Substring(16, 3) + "','" +
                                       r.typeContent.Substring(19, 3) + "','" +
                                       r.typeContent.Substring(22, 3) + "','" +
                                       r.typeContent.Substring(25, 3) + "','" +
                                       loadNum.ToString() + "'," +
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

    };
}

