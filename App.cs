//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Windows.Forms;

//[assembly: log4net.Config.XmlConfigurator(Watch = true)]
//namespace Logger
//{

//    public class App
//    {
//        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("App.cs");

//        public static Project Prj = LoggerFactory.Create_Project();

//        public App()
//        {
//            //
//        }

//        public string getRecord(string logKey, string logID, string projectKey, string recValue)
//        {
//            string recType = "";

//            string[] tmpTypes = recValue.Split((char)0x1c);

//            for (int row = 0; row < App.Prj.RecordTypes.Length / 4; row++)
//            {
//                if (tmpTypes[0].Contains(App.Prj.RecordTypes[row, 0]) &&
//                   (App.Prj.RecordTypes[row, 1] == "0"))
//                {
//                    recType = App.Prj.RecordTypes[row, 3];
//                    break;
//                }
//                if (tmpTypes[0].Contains(App.Prj.RecordTypes[row, 0]) &&
//                (App.Prj.RecordTypes[row, 2] == tmpTypes[Convert.ToInt32(App.Prj.RecordTypes[row, 1])].Substring(0, App.Prj.RecordTypes[row, 2].Length)))
//                {
//                    recType = App.Prj.RecordTypes[row, 3];
//                    break;
//                }
//            }
//            return recType;
//        }

//        public void WriteLog(string filePath, string fileName, string logLine)
//        {
//            string f = filePath + fileName;

//            // does the file exist?
//            if (!File.Exists(f))
//            {
//                // create the file
//                using (StreamWriter sw = File.CreateText(f))
//                {
//                    sw.WriteLine("============ " + DateTime.Now.ToString() + "=============");
//                }


//            }
//            else
//            {

//                using (StreamWriter sw = File.AppendText(f))
//                {
//                    string month = String.Format("{0}", DateTime.Now.Month.ToString("D2"));
//                    string day = String.Format("{0}", DateTime.Now.Day.ToString("D2"));
//                    string year = String.Format("{0}", DateTime.Now.Year.ToString("D2"));
//                    string hour = String.Format("{0}", DateTime.Now.Hour.ToString("D2"));
//                    string min = String.Format("{0}", DateTime.Now.Minute.ToString("D2"));
//                    string sec = String.Format("{0}", DateTime.Now.Second.ToString("D2"));
//                    string milisec = String.Format("{0}", DateTime.Now.Millisecond.ToString("D3"));

//                    sw.WriteLine(month + day + year + " " + hour + min + sec + milisec + " [ " + logLine);
//                }
//            }

//        }

//        public bool ValidateYesNo(string value)
//        {
//            if (!(value == "000" || value == "001"))
//            {
//                Console.WriteLine("Value not in range 000 or 001");
//                return false;
//            }
//            return true;
//        }

//        public bool ValidateRange(string value, int start, int end)
//        {
//            int valSta = Convert.ToInt32(value);
//            if (!(valSta <= end && valSta >= start))
//            {
//                Console.WriteLine("Value out of range");
//                return false;
//            }
//            return true;
//        }

//        public virtual Dictionary<string, string> readData(string sql)
//        {
//            DataTable dt = new DataTable();
//            DbCrud db = new DbCrud();
//            dt = db.GetTableFromDb(sql);
//            Dictionary<string, string> dicData = new Dictionary<string, string>();

//            if (dt.Rows.Count > 0)
//            {
//                foreach (DataRow row in dt.Rows)
//                {
//                    dicData.Add(row[1].ToString() + Convert.ToInt64(row[0]).ToString(), row[0].ToString());
//                }
//            }
//            return dicData;
//        }

//        public string showBytes(byte[] data)
//        {
//            string stringData = Encoding.ASCII.GetString(data);
//            StringBuilder sb = new StringBuilder();

//            foreach (char b in stringData)
//            {
//                if (b == 0x1C)
//                {
//                    sb.Append("[FS]");
//                }
//                else if (b == 0x1D)
//                {
//                    sb.Append("[GS]");
//                }
//                else
//                {
//                    sb.Append(b);
//                }
//            }

//            return sb.ToString();
//        }

//        internal string getOptionDescription(DataTable dataTable, string field, string fieldValue)
//        {

//            // todo: enter data descriptions for all records

//            string optionDesc = "";
//            string fieldDesc = "";
//            string scriptID = "";

//            // MLH If field has no value then no need to get description

//            if (fieldValue.Trim() == "" || fieldValue == null) return optionDesc;

//            // what's the description of the field
//            foreach (DataRow item in dataTable.Rows)
//            {
//                if (item[2].ToString().Trim() == field)
//                {
//                    optionDesc = item[3].ToString().Trim();
//                    if (item[5].ToString() != null && item[5].ToString().Trim() != "")
//                    {
//                        Digester myDigester = LoggerFactory.Create_Digester();
//                        fieldDesc = myDigester.fieldDigester(item[5].ToString(), fieldValue, null);
//                        fieldValue = fieldValue.Replace(";", " ");
//                    }
//                    if (item[4].ToString().Length > 0 && item[4].ToString().Substring(0, 1) == "{")
//                    {
//                        Regex handleBars = new Regex(@"(\{.*?\})", RegexOptions.Singleline);
//                        MatchCollection scriptsToApply = handleBars.Matches(item[4].ToString());
//                        if (scriptsToApply.Count > 0)
//                            scriptID = scriptsToApply[0].ToString().Substring(1, 1);

//                        Digester myDigester = LoggerFactory.Create_Digester();
//                        optionDesc += myDigester.fieldDigester(scriptID, fieldValue, item[4].ToString());
//                    }
//                    else
//                    {
//                        optionDesc += " = " + fieldValue;

//                        if ((item[4].ToString() == "") && (fieldDesc != ""))
//                            optionDesc += fieldDesc;
//                        else
//                            optionDesc += insertDescription(item[4].ToString()) + fieldDesc;
//                    }
//                    break;
//                }
//            }
//            return optionDesc;
//        }

//        internal string insertDescription(string fieldDescription)
//        {
//            string description = "";

//            if (fieldDescription != "")
//            {
//                if (fieldDescription.Contains("\r\n"))
//                {
//                    description += System.Environment.NewLine + fieldDescription.Trim() + System.Environment.NewLine;
//                }
//                else
//                {
//                    description += " " + fieldDescription.Trim() + System.Environment.NewLine;
//                }
//            }
//            else
//            {
//                description += fieldDescription.Trim() + "\t" + System.Environment.NewLine;
//            }
//            return description;
//        }

//        internal string parseToView()
//        {
//            return null;
//        }

//        internal LoggerProgressBar1.LoggerProgressBar1 getLoggerProgressBar()
//        {
//            LoggerProgressBar1.LoggerProgressBar1 lpb = null;

//            if (Application.OpenForms["ProjectData"].Controls.Find("LoggerProgressBar1", true).Length == 0)
//            {
//                lpb = new LoggerProgressBar1.LoggerProgressBar1();
//                Application.OpenForms["ProjectData"].Controls.Add(lpb);
//            }
//            else
//            {
//                Control[] ca = Application.OpenForms["ProjectData"].Controls.Find("LoggerProgressBar1", true);
//                lpb = (LoggerProgressBar1.LoggerProgressBar1)ca[0];
//                lpb.Value = 0;
//            }

//            lpb.Dock = DockStyle.Bottom;
//            lpb.Visible = true;
//            lpb.LblTitle = this.ToString();

//            return lpb;
//        }

//    }
//}

using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Logger
{
    public enum menusTypes
    {
        ProjectOptions = 4,
        ProjectData = 5,
        ScanOptions = 6,
        LogViewLogs = 7,
        LogViewFiles = 8,
        LogViewFilter = 9,
    }

    public class App
    {
        private static readonly ILog log = LogManager.GetLogger("App.cs");
        public static Project Prj = LoggerFactory.Create_Project();

        public string getRecord(string logKey, string logID, string projectKey, string recValue)
        {
            string record = "";
            string[] strArray = recValue.Split('\u001C');
            for (int index = 0; index < App.Prj.RecordTypes.Length / 4; ++index)
            {
                if (strArray[0].Contains(App.Prj.RecordTypes[index, 0]) && App.Prj.RecordTypes[index, 1] == "0")
                {
                    record = App.Prj.RecordTypes[index, 3];
                    break;
                }
                if (strArray[0].Contains(App.Prj.RecordTypes[index, 0]) && App.Prj.RecordTypes[index, 2] == strArray[Convert.ToInt32(App.Prj.RecordTypes[index, 1])].Substring(0, App.Prj.RecordTypes[index, 2].Length))
                {
                    record = App.Prj.RecordTypes[index, 3];
                    break;
                }
            }
            return record;
        }

        public void WriteLog(string filePath, string fileName, string logLine)
        {
            string path = filePath + fileName;
            if (!File.Exists(path))
            {
                using (StreamWriter text = File.CreateText(path))
                    text.WriteLine("============ " + DateTime.Now.ToString() + "=============");
            }
            else
            {
                using (StreamWriter streamWriter = File.AppendText(path))
                {
                    DateTime now = DateTime.Now;
                    string str1 = string.Format("{0}", (object)now.Month.ToString("D2"));
                    now = DateTime.Now;
                    string str2 = string.Format("{0}", (object)now.Day.ToString("D2"));
                    now = DateTime.Now;
                    string str3 = string.Format("{0}", (object)now.Year.ToString("D2"));
                    now = DateTime.Now;
                    int num = now.Hour;
                    string str4 = string.Format("{0}", (object)num.ToString("D2"));
                    now = DateTime.Now;
                    num = now.Minute;
                    string str5 = string.Format("{0}", (object)num.ToString("D2"));
                    now = DateTime.Now;
                    num = now.Second;
                    string str6 = string.Format("{0}", (object)num.ToString("D2"));
                    now = DateTime.Now;
                    num = now.Millisecond;
                    string str7 = string.Format("{0}", (object)num.ToString("D3"));
                    streamWriter.WriteLine(str1 + str2 + str3 + " " + str4 + str5 + str6 + str7 + " [ " + logLine);
                }
            }
        }

        public bool ValidateYesNo(string value)
        {
            if (value == "000" || value == "001")
                return true;
            Console.WriteLine("Value not in range 000 or 001");
            return false;
        }

        public bool ValidateRange(string value, int start, int end)
        {
            int int32 = Convert.ToInt32(value);
            if (int32 <= end && int32 >= start)
                return true;
            Console.WriteLine("Value out of range");
            return false;
        }

        public virtual Dictionary<string, string> readData(string sql)
        {
            DataTable dataTable = new DataTable();
            DataTable tableFromDb = new DbCrud().GetTableFromDb(sql);
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (tableFromDb.Rows.Count > 0)
            {
                foreach (DataRow row in (InternalDataCollectionBase)tableFromDb.Rows)
                    dictionary.Add(row[1].ToString() + Convert.ToInt64(row[0]).ToString(), row[0].ToString());
            }
            return dictionary;
        }

        public string showBytes(byte[] data)
        {
            string str = Encoding.ASCII.GetString(data);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char ch in str)
            {
                switch (ch)
                {
                    case '\u001C':
                        stringBuilder.Append("[FS]");
                        break;
                    case '\u001D':
                        stringBuilder.Append("[GS]");
                        break;
                    default:
                        stringBuilder.Append(ch);
                        break;
                }
            }
            return stringBuilder.ToString();
        }

        internal string getOptionDescription(DataTable dataTable, string field, string fieldValue)
        {
            string optionDescription = "";
            string str = "";
            string fieldType = "";
            if (fieldValue.Trim() == "" || fieldValue == null)
                return optionDescription;
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                if (row[2].ToString().Trim() == field)
                {
                    optionDescription = row[3].ToString().Trim();
                    if (row[5].ToString() != null && row[5].ToString().Trim() != "")
                    {
                        str = LoggerFactory.Create_Digester().fieldDigester(row[5].ToString(), fieldValue, (string)null);
                        fieldValue = fieldValue.Replace(";", " ");
                    }
                    if (row[4].ToString().Length > 0 && row[4].ToString().Substring(0, 1) == "{")
                    {
                        MatchCollection matchCollection = new Regex("(\\{.*?\\})", RegexOptions.Singleline).Matches(row[4].ToString());
                        if (matchCollection.Count > 0)
                            fieldType = matchCollection[0].ToString().Substring(1, 1);
                        Digester digester = LoggerFactory.Create_Digester();
                        optionDescription += digester.fieldDigester(fieldType, fieldValue, row[4].ToString());
                        break;
                    }
                    optionDescription = optionDescription + " = " + fieldValue;
                    optionDescription = !(row[4].ToString() == "") || !(str != "") ? optionDescription + this.insertDescription(row[4].ToString()) + str : optionDescription + str;
                    break;
                }
            }
            return optionDescription;
        }

        internal string insertDescription(string fieldDescription)
        {
            string str = "";
            return !(fieldDescription != "") ? str + fieldDescription.Trim() + "\t" + Environment.NewLine : (!fieldDescription.Contains("\r\n") ? str + " " + fieldDescription.Trim() + Environment.NewLine : str + Environment.NewLine + fieldDescription.Trim() + Environment.NewLine);
        }

        internal string parseToView() => (string)null;

        internal LoggerProgressBar1.LoggerProgressBar1 getLoggerProgressBar()
        {
            LoggerProgressBar1.LoggerProgressBar1 loggerProgressBar;
            if (Application.OpenForms["ProjectData"].Controls.Find("LoggerProgressBar1", true).Length == 0)
            {
                loggerProgressBar = new LoggerProgressBar1.LoggerProgressBar1();
                Application.OpenForms["ProjectData"].Controls.Add((Control)loggerProgressBar);
            }
            else
            {
                loggerProgressBar = (LoggerProgressBar1.LoggerProgressBar1)Application.OpenForms["ProjectData"].Controls.Find("LoggerProgressBar1", true)[0];
                loggerProgressBar.Value = 0;
            }
            loggerProgressBar.Dock = DockStyle.Bottom;
            loggerProgressBar.Visible = true;
            loggerProgressBar.LblTitle = this.ToString();
            return loggerProgressBar;
        }

        internal void MenuPermissions(
          string permissions,
          ToolStripItemCollection menuItemsCollection,
          menusTypes menuType)
        {
            if (permissions == null || menuItemsCollection == null)
                return;
            string[] strArray = permissions.Split('\n');
            string str1 = strArray[(int)menuType].Substring(strArray[(int)menuType].IndexOf(":") + 2, 8);
            ToolStripItemCollection stripItemCollection = menuItemsCollection;
            int startIndex = 0;
            for (int index = 0; index < stripItemCollection.Count; ++index)
            {
                string str2 = "";
                stripItemCollection[index].Enabled = Convert.ToBoolean(str1.Substring(startIndex, 1) == "1");
                str2 = str1.Substring(startIndex, 1) == "1" ? "true" : "false";
                if (stripItemCollection[index].GetType().ToString() != "System.Windows.Forms.ToolStripSeparator")
                    ++startIndex;
            }
        }
    }
}

