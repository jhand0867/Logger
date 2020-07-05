using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Logger
{
    public class App
    {
        public static Project Prj = new Project();
        public App()
        {
            //

        }

        public DataTable getRecord(string logKey, string logID, string projectKey, string recValue)
        {
            List<typeRec> typeList = new List<typeRec>();
            string[] tmpTypes;
            int recCount = 0;
            string recType = "";
            DataTable dataTable = new DataTable();

            foreach (string recordType in App.Prj.RecordTypes)
            {
                if (recValue.Contains(recordType))
                {
                    if (recCount == 0 || recCount == 1)
                    {
                        recType = recCount.ToString();
                    }
                    else
                    {
                        tmpTypes = recValue.Split((char)0x1c);

                        foreach (string subRecordType in App.Prj.SubRecordTypes)
                        {
                            if (tmpTypes[3] == subRecordType)
                            {
                                recType = subRecordType;
                                break;
                            }

                        }
                    }
                    break;
                }
                recCount++;
            }
            switch (recType)
            {
                case "00":
                    TRec tr = new TRec();
                    break;
                case "01":
                    TReply treply = new TReply();
                    break;
                case "11":
                    screenRec scrRec = new screenRec();
                    break;
                case "12":
                    stateRec staRec = new stateRec();
                    dataTable = staRec.getRecord(logKey, logID, projectKey);
                    break;
                case "13":
                    configParamsRec cpRec = new configParamsRec();
                    break;
                case "15":
                    FitRec fitRec = new FitRec();
                    dataTable = fitRec.getRecord(logKey, logID, projectKey);
                    break;
                case "16":
                    ConfigIdRec cir = new ConfigIdRec();
                    dataTable = cir.getRecord(logKey, logID, projectKey);
                    break;
                case "1A":
                    EnhancedParamsRec epRec = new EnhancedParamsRec();
                    break;
                case "1B":
                    //writeMAC(typeList);
                    break;
                case "1C":
                    DateAndTimeRec dt = new DateAndTimeRec();
                    break;
                case "1E":
                    //writeDispenser(typeList);
                    break;
                case "42":
                    ExtEncryptionRec xer = new ExtEncryptionRec();
                    break;
            }
            return dataTable;
        }
        public void WriteLog(string filePath, string fileName, string logLine)
        {
            string f = filePath + fileName;

            // does the file exist?
            if (!File.Exists(f))
            {
                // create the file
                using (StreamWriter sw = File.CreateText(f))
                {
                    sw.WriteLine("============ " + DateTime.Now.ToString() + "=============");
                }


            }
            else
            {

                using (StreamWriter sw = File.AppendText(f))
                {
                    string month = String.Format("{0}", DateTime.Now.Month.ToString("D2"));
                    string day = String.Format("{0}", DateTime.Now.Day.ToString("D2"));
                    string year = String.Format("{0}", DateTime.Now.Year.ToString("D2"));
                    string hour = String.Format("{0}", DateTime.Now.Hour.ToString("D2"));
                    string min = String.Format("{0}", DateTime.Now.Minute.ToString("D2"));
                    string sec = String.Format("{0}", DateTime.Now.Second.ToString("D2"));
                    string milisec = String.Format("{0}", DateTime.Now.Millisecond.ToString("D3"));

                    sw.WriteLine(month + day + year + " " + hour + min + sec + milisec + " [ " + logLine);
                }
            }

        }
        public bool ValidateYesNo(string value)
        {
            if (!(value == "000" || value == "001"))
            {
                Console.WriteLine("Value not in range 000 or 001");
                return false;
            }
            return true;
        }
        public bool ValidateRange(string value, int start, int end)
        {
            int valSta = Convert.ToInt32(value);
            if (!(valSta <= end && valSta >= start))
            {
                Console.WriteLine("Value out of range");
                return false;
            }
            return true;
        }

        public virtual Dictionary<string, string> readData(string sql)
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

                Dictionary<string, string> dicData = new Dictionary<string, string>();

                while (dataReader.Read())
                {
                    dicData.Add(dataReader.GetString(1) + dataReader.GetInt64(0).ToString(), dataReader.GetString(0));
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

        public string showBytes(byte[] data)
        {
            //string fileBytes = data;
            string stringData = Encoding.ASCII.GetString(data);
            StringBuilder sb = new StringBuilder();

            foreach (char b in stringData)
            {
                if (b == 0x1C)
                {
                    sb.Append("[FS]");
                }
                else if (b == 0x1D)
                {
                    sb.Append("[GS]");
                }
                else
                {
                    sb.Append(b);
                }
            }

            return sb.ToString();
        }
    }
}
