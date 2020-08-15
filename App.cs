using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Logger
{
    public class App
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        //                        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("App.cs");
        public static Project Prj = new Project();
        public App()
        {
            //

        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey, string recValue)
        {
            List<typeRec> typeList = new List<typeRec>();
            string[] tmpTypes;
            int recCount = 0;
            string recType = "";
            DataTable dataTable = new DataTable();
            List<DataTable> dts = new List<DataTable>();

            foreach (string recordType in App.Prj.RecordTypes)
            {
                if (recValue.Contains(recordType))
                {
                    // mlh check type of message 
                    switch (recCount)
                    {
                        case 0:
                        case 1:
                            recType = recCount.ToString("00");
                            break;
                        case 2:
                            tmpTypes = recValue.Split((char)0x1c);
                            foreach (string subRecordType in App.Prj.SubRecordTypes3)
                            {
                                if (tmpTypes[3] == subRecordType)
                                {
                                    recType = subRecordType;
                                    break;
                                }

                            }
                            break;

                        case 3:
                            tmpTypes = recValue.Split((char)0x1c);
                            foreach (string subRecordType in App.Prj.SubRecordTypes8)
                            {
                                if (tmpTypes[2] == subRecordType)
                                {
                                    recType = "8" + subRecordType;
                                    break;
                                }
                            }
                            break;
                    }
                }
                recCount++;
                }

            switch (recType)
            {
                // mlh process type of message

                case "00":
                    TRec tr = new TRec();
                    dts = tr.getRecord(logKey, logID, projectKey);
                    break;
                case "01":
                    TReply treply = new TReply();
                    dts = treply.getRecord(logKey, logID, projectKey);
                    break;
                case "11":
                    screenRec scrRec = new screenRec();
                    break;
                case "12":
                    stateRec staRec = new stateRec();
                    dts = staRec.getRecord(logKey, logID, projectKey);
                    break;
                case "13":
                    configParamsRec cpRec = new configParamsRec();
                    break;
                case "15":
                    FitRec fitRec = new FitRec();
                    dts = fitRec.getRecord(logKey, logID, projectKey);
                    break;
                case "16":
                    ConfigIdRec cir = new ConfigIdRec();
                    dts = cir.getRecord(logKey, logID, projectKey);
                    break;
                case "1A":
                    EnhancedParamsRec epRec = new EnhancedParamsRec();
                    dts = epRec.getRecord(logKey, logID, projectKey);
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
                case "81":
                    ICCCurrencyDOT iccCurrency = new ICCCurrencyDOT();
                    dts = iccCurrency.getRecord(logKey, logID, projectKey);
                    break;
                case "82":
                    ICCTransactionDOT iccTransaction = new ICCTransactionDOT();
                    dts = iccTransaction.getRecord(logKey, logID, projectKey);
                    break;
                case "83":
                    ICCLanguageSupportT iccLanguage = new ICCLanguageSupportT();
                    dts = iccLanguage.getRecord(logKey, logID, projectKey);
                    break;
                case "84":
                    ICCTerminalDOT iccTerminal = new ICCTerminalDOT();
                    dts = iccTerminal.getRecord(logKey, logID, projectKey);
                    break;
                case "85":
                    ICCApplicationIDT iccApplication = new ICCApplicationIDT();
                    dts = iccApplication.getRecord(logKey, logID, projectKey);
                    break;

            }
            return dts;
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
    }
}
