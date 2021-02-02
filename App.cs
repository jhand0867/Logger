using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Logger
{
    public class App
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("App.cs");

        public static Project Prj = MessageFactory.Create_Project();

        public App()
        {
            //
        }

        public string getRecord(string logKey, string logID, string projectKey, string recValue)
        {
            string recType = "";

            string[] tmpTypes = recValue.Split((char)0x1c);

            for (int row = 0; row < App.Prj.RecordTypes.Length / 4; row++)
            {
                if (tmpTypes[0].Contains(App.Prj.RecordTypes[row, 0]) &&
                   (App.Prj.RecordTypes[row, 1] == "0"))
                {
                    recType = App.Prj.RecordTypes[row, 3];
                    break;
                }
                if (tmpTypes[0].Contains(App.Prj.RecordTypes[row, 0]) &&
                (App.Prj.RecordTypes[row, 2] == tmpTypes[Convert.ToInt32(App.Prj.RecordTypes[row, 1])].Substring(0, App.Prj.RecordTypes[row, 2].Length)))
                {
                    recType = App.Prj.RecordTypes[row, 3];
                    break;
                }
            }
            return recType;
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
            // here mlh

            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            Dictionary<string, string> dicData = new Dictionary<string, string>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dicData.Add(row[1].ToString() + Convert.ToInt64(row[0]).ToString(), row[0].ToString());
                }
            }
            return dicData;
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

        internal string getOptionDescription(DataTable dataTable, string field)
        {
            string optionDesc = "";
            // what's the description of the field
            foreach (DataRow item in dataTable.Rows)
            {
                if (item[2].ToString().Trim() == field)
                {
                    optionDesc = item[3].ToString().Trim();
                    break;
                }
            }
            return optionDesc;
        }
    }
}
