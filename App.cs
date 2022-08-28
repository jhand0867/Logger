using log4net;
using LoggerProgressBar1;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
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
        //private static readonly ILog log = LogManager.GetLogger("App.cs");
        private static readonly ILog log = LogManager.GetLogger(
                                           System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


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

        //// DescriptionData requires a fieldName and should have either 
        //// fieldDescription (row[4]), fieldType (row[5]), or scriptValue (row[7]). 
        ///  It can not have a combination of them,
        //// except for rectype 'X', which can have fieldDescription and fieldType

        internal string getOptionDescription(DataTable dataTable, string field, string fieldValue)
        {
            string optionDescription = "";
            string str = "";
            if (fieldValue.Trim() == "" || fieldValue == null)
                return optionDescription;

            foreach (DataRow row in dataTable.Rows)
            {
                if (row[2].ToString().Trim() == field)
                {
                    string[] descriptionFields = new string[] { "", "", "" };

                    descriptionFields[0] = row[3].ToString().Trim();

                    if (!string.IsNullOrEmpty(row[4].ToString().Trim()))
                    {
                        descriptionFields[1] = fieldValue;
                        descriptionFields[2] = row[4].ToString().Trim();
                        optionDescription = insertRowRtf(descriptionFields);
                        break;
                    }

                    if (row[6].ToString().Length == 2 && !string.IsNullOrEmpty(row[5].ToString().Trim()))
                    {
                        str = LoggerFactory.Create_Digester().fieldDigester(row[6].ToString(), fieldValue, row[5].ToString());
                        fieldValue = fieldValue.Replace(";", " ");

                        descriptionFields[1] = fieldValue;

                        if (str.Contains("\\intbl "))
                        {
                            descriptionFields[2] = "";
                            optionDescription = insertRowRtf(descriptionFields);
                            optionDescription += str;
                            break;
                        }
                        descriptionFields[2] = str;
                        optionDescription = insertRowRtf(descriptionFields);
                        break;
                    }

                    if (row[6].ToString().Length == 2 && row[7].ToString().Length > 0 && row[7].ToString().Substring(0, 1) == "{")
                    {
                        str = LoggerFactory.Create_Digester().fieldDigester(row[6].ToString(), fieldValue, row[7].ToString());

                        descriptionFields[1] = fieldValue;

                        if (str.Contains("\\intbl "))
                        {
                            descriptionFields[2] = "";
                            optionDescription = insertRowRtf(descriptionFields);
                            optionDescription += str;
                            break;
                        }
                        descriptionFields[2] = str;
                        optionDescription = insertRowRtf(descriptionFields);
                        break;
                    }

                    descriptionFields[1] = fieldValue;
                    descriptionFields[2] = "";
                    optionDescription = insertRowRtf(descriptionFields);

                }
            }
            return optionDescription;
        }


        internal string insertRowRtf(string[] rows)
        {
            string str = "";
            if ((rows[1].Length == 0) && (rows[2].Length == 0))
            {
                string tableDef = @"\trowd\trgaph80\clbrdrt\brdrw10\brdrs\clbrdrl\brdrw10\brdrs\clbrdrb\brdrw10\brdrs\clbrdrr\brdrw10\brdrs \cellx9000";
                str = tableDef + @" \intbl " + rows[0] + @" \cell \row ";
            }
            else
            if ((rows[2].Length == 0) && (rows[1].Length > 23))
            {
                string tableDef = @"\trowd\trgaph80\clbrdrt\brdrw10\brdrs\clbrdrl\brdrw10\brdrs\clbrdrb\brdrw10\brdrs\clbrdrr\brdrw10\brdrs \cellx3000\clbrdrt\brdrw10\brdrs\clbrdrl\brdrw10\brdrs\clbrdrb\brdrw10\brdrs\clbrdrr\brdrw10\brdrs \cellx9000";
                str = tableDef + @" \intbl " + rows[0] + @" \cell " + rows[1] + @" \cell \row ";
            }
            else
            {
                string tableDef = @"\trowd\trgaph80\clbrdrt\brdrw10\brdrs\clbrdrl\brdrw10\brdrs\clbrdrb\brdrw10\brdrs\clbrdrr\brdrw10\brdrs \cellx3000\clbrdrt\brdrw10\brdrs\clbrdrl\brdrw10\brdrs\clbrdrb\brdrw10\brdrs\clbrdrr\brdrw10\brdrs \cellx5000\clbrdrt\brdrw10\brdrs\clbrdrl\brdrw10\brdrs\clbrdrb\brdrw10\brdrs\clbrdrr\brdrw10\brdrs \cellx9000";
                str = tableDef + @" \intbl " + rows[0] + @" \cell " + rows[1] + @" \cell " + rows[2] + @" \cell \row ";
            }
            return str;
        }

        internal string insertRowRtf(string[] rows, int numberOfColums)
        {
            string str, tableDef = "";
            switch (numberOfColums)
            {
                case 1:
                    tableDef = @"\trowd\trgaph80\clbrdrt\brdrw10\brdrs\clbrdrl\brdrw10\brdrs\clbrdrb\brdrw10\brdrs\clbrdrr\brdrw10\brdrs \cellx9000";
                    str = tableDef + @" \intbl " + rows[0] + @" \cell \row ";
                    break;
                case 2:
                    tableDef = @"\trowd\trgaph80\clbrdrt\brdrw10\brdrs\clbrdrl\brdrw10\brdrs\clbrdrb\brdrw10\brdrs\clbrdrr\brdrw10\brdrs \cellx3000\clbrdrt\brdrw10\brdrs\clbrdrl\brdrw10\brdrs\clbrdrb\brdrw10\brdrs\clbrdrr\brdrw10\brdrs \cellx9000";
                    str = tableDef + @" \intbl " + rows[0] + @" \cell " + rows[1] + @" \cell \row ";
                    break;
                default: // default is to print 3 columns
                    tableDef = @"\trowd\trgaph80\clbrdrt\brdrw10\brdrs\clbrdrl\brdrw10\brdrs\clbrdrb\brdrw10\brdrs\clbrdrr\brdrw10\brdrs \cellx3000\clbrdrt\brdrw10\brdrs\clbrdrl\brdrw10\brdrs\clbrdrb\brdrw10\brdrs\clbrdrr\brdrw10\brdrs \cellx5000\clbrdrt\brdrw10\brdrs\clbrdrl\brdrw10\brdrs\clbrdrb\brdrw10\brdrs\clbrdrr\brdrw10\brdrs \cellx9000";
                    str = tableDef + @" \intbl " + rows[0] + @" \cell " + rows[1] + @" \cell " + rows[2] + @" \cell \row ";
                    break;
            }
            return str;
        }

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





