using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;

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
     

        public string StateNumber
        {
            get { return pStateNum; }
            set { pStateNum = value; }
        }
        public string StateType
        {
            get { return pStateType; }
            set { pStateType = value; }
        }
        public string Val1
        {
            get { return pSta1; }
            set { pSta1 = value; }
        }
        public string Val2
        {
            get { return pSta2; }
            set { pSta2 = value; }
        }
        public string Val3
        {
            get { return pSta3; }
            set { pSta3 = value; }
        }
        public string Val4
        {
            get { return pSta4; }
            set { pSta4 = value; }
        }
        public string Val5
        {
            get { return pSta5; }
            set { pSta5 = value; }
        }
        public string Val6
        {
            get { return pSta6; }
            set { pSta6 = value; }
        }
        public string Val7
        {
            get { return pSta7; }
            set { pSta7 = value; }
        }
        public string Val8
        {
            get { return pSta8; }
            set { pSta8 = value; }
        }


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
                    sr.pStateNum = dataReader.GetString(3);
                    sr.pStateType = dataReader.GetString(4);
                    sr.pSta1 = dataReader.GetString(5);
                    sr.pSta2 = dataReader.GetString(6);
                    sr.pSta3 = dataReader.GetString(7);
                    sr.pSta4 = dataReader.GetString(8);
                    sr.pSta5 = dataReader.GetString(9);
                    sr.pSta6 = dataReader.GetString(10);
                    sr.pSta7 = dataReader.GetString(11);
                    sr.pSta8 = dataReader.GetString(12);

                    dicData.Add(dataReader.GetString(1) + dataReader.GetInt64(0).ToString(), sr);
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
        public DataTable getRecord(string logKey, string logID, string projectKey)
        {
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            try
            {
                cnn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM stateInfo WHERE prjkey = '" +
                                                               projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" +
                                                               logKey + "%'", cnn))
                {
                    sda.Fill(dt);

                    return dt;
                }
            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());
                return null;
            }

        }

        public string getInfo(stateRec stRec)
        {
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            DataTable dt = new DataTable();

            string stateType = stRec.StateType;
            string stateNum = "";

            // if current state is an extension, find the state number in the extension list and 
            // get the parent state

            if ( stateType == "Z")
            {
                foreach (string item in App.Prj.ExtensionsLst)
                {
                    if (item.Substring(5, 3) == stRec.stateNum)
                    {
                        stateType = item.Substring(0, 2);
                        stateNum = item.Substring(2, 3);
                        App.Prj.ExtensionsLst.Remove(item);

                        switch (stateType)
                        {
                            case "J ":
                                stateType = "J1";
                                break;
                            case "Y ":
                                stateType = "Y1";
                                break;
                            case "D ":
                                stateType = "D1";
                                break;
                            case "J1":
                                stateType = "J2";
                                break;
                            case "Y1":
                                stateType = "Y2";
                                break;
                            case "D1":
                                stateType = "D2";
                                break;

                        }
                        break;
                    }
                }

            }

            try
            {
                cnn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [dataDescription] WHERE recType = '" + "S"
                    + "' AND subRecType = '" + stateType + "'", cnn))
                {
                    sda.Fill(dt);
                }
            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());
                return null;
            }
            string[] stRecVal = new string[10];

            stRecVal[0] = stRec.StateNumber;
            stRecVal[1] = stateType.PadRight(2);
            stRecVal[2] = stRec.Val1;
            stRecVal[3] = stRec.Val2;
            stRecVal[4] = stRec.Val3;
            stRecVal[5] = stRec.Val4;
            stRecVal[6] = stRec.Val5;
            stRecVal[7] = stRec.Val6;
            stRecVal[8] = stRec.Val7;
            stRecVal[9] = stRec.Val8;

            if (dt.Rows.Count > 0)
            {
                string fieldData = dt.Rows[0][3].ToString().Trim() + ":\t" + stRec.StateNumber + System.Environment.NewLine;

                if (stateType == stRec.StateType)                
                    fieldData += dt.Rows[1][3].ToString().Trim() + ":\t" + stRec.StateType + System.Environment.NewLine;
                else
                    fieldData += dt.Rows[1][3].ToString().Trim() + ":\t" + stRec.StateType  + " ext: " + stateType.Substring(0,1) + " " + stateNum + System.Environment.NewLine;

                fieldData += dt.Rows[2][3].ToString().Substring(0, 40) +  stRec.Val1 + getDescription(dt.Rows[2][4].ToString());
                fieldData += dt.Rows[3][3].ToString().Substring(0, 40) +  stRec.Val2 + getDescription(dt.Rows[3][4].ToString());
                fieldData += dt.Rows[4][3].ToString().Substring(0, 40) + stRec.Val3 + getDescription(dt.Rows[4][4].ToString());
                fieldData += dt.Rows[5][3].ToString().Substring(0, 40) + stRec.Val4 + getDescription(dt.Rows[5][4].ToString());
                fieldData += dt.Rows[6][3].ToString().Substring(0, 40) + stRec.Val5 + getDescription(dt.Rows[6][4].ToString());
                fieldData += dt.Rows[7][3].ToString().Substring(0, 40) +  stRec.Val6 + getDescription(dt.Rows[7][4].ToString());
                fieldData += dt.Rows[8][3].ToString().Substring(0, 40) +  stRec.Val7 + getDescription(dt.Rows[8][4].ToString());
                fieldData += dt.Rows[9][3].ToString().Substring(0, 40) + stRec.Val8 + getDescription(dt.Rows[9][4].ToString());
                fieldData += System.Environment.NewLine;

                // check if an extension in the current state exist

                for (int i = 0; i < 10; i++)
                {
                    if (dt.Rows[i][3].ToString().Contains("Extension") && stRecVal[i] != "000")
                    {
                        App.Prj.ExtensionsLst.Add(stRecVal[1] + stRec.StateNumber + stRecVal[i] );
                    }
                }

                return fieldData;
            }
            else
            {
                return "";
            }

        }

        private string getDescription(string fieldDescription)
        {
            string description = "";

            if (fieldDescription != "")
            {
                description += System.Environment.NewLine + fieldDescription.Trim() + System.Environment.NewLine;
            }
            else
            {
                description += fieldDescription.Trim() + System.Environment.NewLine;
            }
            return description;
        }

    };
}

