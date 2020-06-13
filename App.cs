using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace Logger
{
    public class App
    {
        public static Project Prj = new Project();
        public App()
        {
            //

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
    }
}
