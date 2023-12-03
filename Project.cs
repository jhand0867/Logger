using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logger
{
    public struct dataLine
    {
        public string[] allGroups;
    }

    public struct typeRec
    {
        public string typeIndex;
        public string typeContent;
        public string typeAddData;
    }

    public class Project : App
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private string pKey;
        private string pName;
        private string pBrief;
        private int pLogs;
        private LoggerLicense.LoggerLic pLicense;
        private string pPermissions;
        private string pVersion;
        private bool pAdmin;
        private readonly string[,] recordTypes = new string[27, 4]
        {
          {
            "ATM2HOST: 11",
            "0",
            "",
            "11"
          },
          {
            "HOST2ATM: 4",
            "0",
            "",
            "4"
          },
          {
            "HOST2ATM: 3",
            "3",
            "11",
            "311"
          },
          {
            "HOST2ATM: 3",
            "3",
            "12",
            "312"
          },
          {
            "HOST2ATM: 3",
            "3",
            "13",
            "313"
          },
          {
            "HOST2ATM: 3",
            "3",
            "15",
            "315"
          },
          {
            "HOST2ATM: 3",
            "3",
            "16",
            "316"
          },
          {
            "HOST2ATM: 3",
            "3",
            "1A",
            "31A"
          },
          {
            "HOST2ATM: 3",
            "3",
            "1B",
            "31B"
          },
          {
            "HOST2ATM: 3",
            "3",
            "1C",
            "31C"
          },
          {
            "HOST2ATM: 3",
            "3",
            "1E",
            "31E"
          },
          {
            "HOST2ATM: 8",
            "2",
            "1",
            "81"
          },
          {
            "HOST2ATM: 8",
            "2",
            "2",
            "82"
          },
          {
            "HOST2ATM: 8",
            "2",
            "3",
            "83"
          },
          {
            "HOST2ATM: 8",
            "2",
            "4",
            "84"
          },
          {
            "HOST2ATM: 8",
            "2",
            "5",
            "85"
          },
          {
            "ATM2HOST: 22",
            "0",
            "",
            "22"
          },
          {
            "ATM2HOST: 12",
            "0",
            "",
            "12"
          },
          {
            "ATM2HOST: 23",
            "0",
            "",
            "23"
          },
          {
            "ATM2HOST: 61",
            "0",
            "",
            "61H"
          },
          {
            "HOST2ATM: 6",
            "3",
            "1",
            "61J"
          },
          {
            "HOST2ATM: 6",
            "3",
            "2",
            "62"
          },
          {
            "HOST2ATM: 6",
            "3",
            "3",
            "63"
          },
          {
            "HOST2ATM: 3",
            "3",
            "4",
            "34"
          },
          {
            "HOST2ATM: 1",
            "0",
            "",
            "1"
          },
          {
            "HOST2ATM: 3",
            "3",
            "2",
            "32"
          },
          {
            "HOST2ATM: 3",
            "3",
            "1V",
            "31V"
          }
        };

        private List<StateData> extensionsLst = new List<StateData>();
        public Dictionary<string, string> recTypesDic = new Dictionary<string, string>();

        public string Key
        {
            get => this.pKey;
            set => this.pKey = value;
        }

        public string Name
        {
            get => this.pName;
            set => this.pName = value;
        }

        public string Brief
        {
            get => this.pBrief;
            set => this.pBrief = value;
        }

        public int Logs => this.pLogs;

        public List<StateData> ExtensionsLst
        {
            get => this.extensionsLst;
            set => this.extensionsLst = value;
        }

        public string[,] RecordTypes => this.recordTypes;

        public LoggerLicense.LoggerLic LicenseKey
        {
            get => this.pLicense;
            set => this.pLicense = value;
        }

        public string Permissions
        {
            get => this.pPermissions;
            set => this.pPermissions = value;
        }
        public bool Admin { get => pAdmin; set => pAdmin = value; }
        public string Version { get => pVersion; set => pVersion = value; }

        // private License getLicense() => new License().VerifyLicenseRegistry();

        public Project()
        {
            Project.log.Info((object)"Accessing Project Data");
            this.pKey = "";
            this.pName = "";
            this.pBrief = "";
            this.pLogs = 0;

            //  this.recTypesDic.Add("11", "treq, Transaction Request");   IDEA1

            this.recTypesDic.Add("11", "treq");
            this.recTypesDic.Add("4", "treply");
            this.recTypesDic.Add("311", "screens");
            this.recTypesDic.Add("312", "states");
            this.recTypesDic.Add("313", "configParametersLoad");
            this.recTypesDic.Add("315", "fit");
            this.recTypesDic.Add("316", "configID");
            this.recTypesDic.Add("31A", "enhancedParametersLoad");
            this.recTypesDic.Add("31B", "mac");
            this.recTypesDic.Add("31C", "dateandtime");
            this.recTypesDic.Add("31E", "dispenserCurrency");
            this.recTypesDic.Add("81", "iccCurrencyDOT");
            this.recTypesDic.Add("82", "iccTransactionDOT");
            this.recTypesDic.Add("83", "iccLanguageSupportT");
            this.recTypesDic.Add("84", "iccTerminalDOT");
            this.recTypesDic.Add("85", "iccApplicationIDT");
            this.recTypesDic.Add("22", "solicitedStatus");
            this.recTypesDic.Add("12", "unsolicitedStatus");
            this.recTypesDic.Add("23", "encryptorInitData");
            this.recTypesDic.Add("61H", "uploadEjData");
            this.recTypesDic.Add("61J", "ejAckBlock");
            this.recTypesDic.Add("62", "ejAckStop");
            this.recTypesDic.Add("63", "ejOptionsTimers");
            this.recTypesDic.Add("34", "extendedEncrypKeyChange");
            this.recTypesDic.Add("1", "terminalCommands");
            this.recTypesDic.Add("32", "interactiveTranResponse");
            this.recTypesDic.Add("31V", "voiceGuidance");
        }

        public Project(string pName, string pBrief) => this.createProject(pName, pBrief);

        public Project getProjectByID(string prjID)
        {
            DataTable dataTable = new DataTable();
            DataTable tableFromDb = new DbCrud().GetTableFromDb("SELECT * FROM project WHERE prjKey='" + prjID + "'");
            Project projectById = new Project();
            projectById.Permissions = App.Prj.Permissions;
            projectById.LicenseKey = App.Prj.LicenseKey;
            if (tableFromDb.Rows.Count > 0)
            {
                foreach (DataRow row in (InternalDataCollectionBase)tableFromDb.Rows)
                {
                    projectById.pKey = row[1].ToString();
                    projectById.Name = row[2].ToString();
                    projectById.Brief = row[3].ToString();
                    projectById.pLogs = Convert.ToInt32(row[4].ToString());
                }
            }
            return projectById;
        }

        public async Task<DataTable> getAllProjectsAsync()
        {
            DataTable dataTable = new DataTable();
            DbCrud dbCrud = new DbCrud();
            dataTable = await dbCrud.GetTableFromDbAsync("SELECT * FROM project");
            return dataTable;

        }

        public DataTable getAllProjects()
        {
            DataTable dataTable = new DataTable();
            return new DbCrud().GetTableFromDb("SELECT * FROM project");
        }

        public Dictionary<string, Project> getAllProjects1()
        {
            DataTable dataTable = new DataTable();
            DataTable tableFromDb = new DbCrud().GetTableFromDb("SELECT * FROM project");
            Dictionary<string, Project> allProjects1 = new Dictionary<string, Project>();
            if (tableFromDb != null && tableFromDb.Rows.Count > 0)
            {
                foreach (DataRow row in (InternalDataCollectionBase)tableFromDb.Rows)
                    allProjects1.Add(row[1].ToString() + Convert.ToInt32(row[0]).ToString(), new Project()
                    {
                        pKey = row[1].ToString(),
                        Name = row[2].ToString(),
                        Brief = row[3].ToString(),
                        pLogs = Convert.ToInt32(row[4])
                    });
            }
            return allProjects1;
        }

        public DataTable getProjectByName(string pName)
        {
            return new DbCrud().GetTableFromDb("SELECT * FROM project WHERE prjName ='" + pName + "'");
        }

        public string getProjectNameByProjectKey(string pKey)
        {
            return new DbCrud().GetScalarStrFromDb("SELECT prjName FROM project WHERE prjKey ='" + pKey + "'");
        }

        public string getProjectIDByName(string pName)
        {
            return new DbCrud().GetScalarStrFromDb("SELECT prjKey FROM project WHERE prjName ='" + pName + "'");
        }

        public bool updateProjectByName(Project project, string pName, string pBrief) =>
            new DbCrud().crudToDb("UPDATE Project SET prjName ='" + pName + "', prjBrief ='" + pBrief + "' WHERE prjKey ='" + project.pKey + "'");

        public bool addLogToProject(string pKey) => new DbCrud().crudToDb("UPDATE Project SET prjLogs = prjLogs + 1 WHERE prjKey ='" + pKey + "'; UPDATE Logs SET uploaded = 1");

        public int attachLogToProject(string pKey, string pFilename) => new DbCrud().GetScalarIntFromDb("INSERT INTO logs(prjKey, logFile, uploadDate) \r\n                    VALUES('" + pKey + "','" + pFilename + "', STRFTIME('%Y-%m-%d %H:%M:%S-%f','Now','LocalTime')); SELECT LAST_INSERT_ROWID() AS int;");

        public Dictionary<string, Project> createProject(string name, string brief)
        {
            this.Name = name;
            this.Brief = brief;
            this.pLogs = 0;
            this.pKey = Guid.NewGuid().ToString();
            DataTable dataTable = new DataTable();
            DataTable tableFromDb = new DbCrud().GetTableFromDb("INSERT INTO Project(prjKey,prjName,prjBrief,prjLogs,createDate)VALUES('" + this.Key.Trim() + "','" + this.Name + "','" + this.Brief + "','" + 0.ToString() + "',STRFTIME('%Y-%m-%d %H:%M:%S-%f','Now','LocalTime'))");
            Dictionary<string, Project> project = new Dictionary<string, Project>();
            if (tableFromDb != null && tableFromDb.Rows.Count > 0)
            {
                foreach (DataRow row in (InternalDataCollectionBase)tableFromDb.Rows)
                    project.Add(row[1].ToString() + Convert.ToInt32(row[0]).ToString(), new Project()
                    {
                        pKey = row[1].ToString(),
                        Name = row[2].ToString(),
                        Brief = row[3].ToString(),
                        pLogs = Convert.ToInt32(row[4])
                    });
            }
            return project;
        }

        public bool deleteProjectByName(string prjName)
        {
            DbCrud dbCrud = new DbCrud();
            string sql1 = "SELECT prjKey FROM project where prjName = '" + prjName + "'";
            string scalarStrFromDb = dbCrud.GetScalarStrFromDb(sql1);

            string sql11 = "SELECT id FROM logs where prjKey = '" + scalarStrFromDb + "'";
            DataTable DT = dbCrud.GetTableFromDb(sql11);
            foreach (DataRow DR in DT.Rows)
            {
                log.Info("Dropping data from all records tables");
                App.Prj.dropDataByLogID(DR["id"].ToString());
            }


            string sql2 = "DELETE FROM loginfo WHERE prjKey = '" + scalarStrFromDb + "'";
            bool db1 = dbCrud.crudToDb(sql2);
            string sql3 = "DELETE FROM project where prjName ='" + prjName + "'";
            bool db2 = dbCrud.crudToDb(sql3);
            string sql4 = "DELETE FROM logs where prjKey = '" + scalarStrFromDb + "'";
            bool db3 = dbCrud.crudToDb(sql4);
            return db1 & db2 & db3;
        }

        private void deleteProjectLogsByID(string prjID) => new DbCrud().crudToDb("DELETE loginfo where prjID = '" + prjID + "'");

        public DataTable getLogDetailByID(string logID) => new DbCrud().GetTableFromDb("SELECT * FROM logDetail WHERE logID = '" + logID + "'");

        public void uploadLog(string filename)
        {
            int project = this.attachLogToProject(this.pKey, filename);
            Dictionary<string, dataLine> dictionary = new Dictionary<string, dataLine>();
            string[] strArray = System.IO.File.ReadAllLines(filename);
            LoggerProgressBar1.LoggerProgressBar1 loggerProgressBar = this.getLoggerProgressBar();
            loggerProgressBar.Maximum = strArray.Length + 1;
            // loggerProgressBar.LblTitle = this.ToString();

            loggerProgressBar.LblTitle = filename.Substring(filename.LastIndexOf("\\") + 1, filename.Length - (filename.LastIndexOf("\\") + 1));

            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            string str1 = "";
            long index1 = 0;
            bool flag1 = false;
            while (index1 < (long)strArray.Length)
            {
                string str2 = strArray[index1];
                ++index1;
                if (str2 == "")
                    ++num3;
                else if (str2.Substring(0, 1) != "[")
                {
                    this.writeLogDetail("", str2, project);
                }
                else
                {
                    Regex regex1 = new Regex("\\d\\d\\d\\d-\\d\\d-\\d\\d.\\d\\d:\\d\\d:\\d\\d-\\d\\d\\d");
                    while (index1 < (long)strArray.Length && strArray[index1].Length > 0 && strArray[index1].Substring(0, 1) != "[" && strArray[index1].Substring(0, 1) != "=" || index1 < (long)strArray.Length && strArray[index1].Length == 0 || index1 < (long)strArray.Length && regex1.Matches(strArray[index1]).Count == 0)
                    {
                        if (strArray[index1] != "")
                            str2 = str2 + strArray[index1] + Environment.NewLine;
                        ++index1;
                        ++num4;
                    }
                    loggerProgressBar.Value += loggerProgressBar.Step;
                    loggerProgressBar.ValueUpdated(loggerProgressBar.Value);
                    if (str2.EndsWith(Environment.NewLine))
                        str2 = str2.Substring(0, str2.Length - 2);
                    if (index1 != 44527L && index1 != 44528L && index1 != 44529L)
                        ;
                    Regex regex2 = new Regex("(\\[.*\\])(\\[.*\\])(\\[.*\\])(\\[.*\\])(\\[.*\\])(\\[.*\\])(\\[.*\\])(\\[.*\\])?(.*)");
                    Regex regex3 = new Regex("(\\[.*\\])(\\[.*\\])(\\[.*\\])(\\[.*\\])(\\[.*\\])(\\[.*\\])(\\[.*\\])?(.*)");
                    Regex regex4 = new Regex("]");
                    MatchCollection matchCollection1 = regex2.Matches(str2);
                    if (matchCollection1.Count == 0 || regex1.Matches(matchCollection1[0].Groups[0].ToString()).Count == 0)
                        matchCollection1 = regex3.Matches(str2);
                    string str3 = "";
                    dataLine data = new dataLine();
                    data.allGroups = new string[11];
                    IEnumerator enumerator = matchCollection1[0].Groups.GetEnumerator();
                    try
                    {
                        if (enumerator.MoveNext())
                        {
                            Match current = (Match)enumerator.Current;
                            string str4 = "";
                            if (str2.Length != current.Value.Length)
                                str4 = str2.Substring(current.Value.Length, str2.Length - current.Value.Length);
                            int index2 = 0;
                            foreach (Group group in current.Groups)
                            {
                                if (group.Value + str4 == str2)
                                {
                                    ++num2;
                                }
                                else
                                {
                                    MatchCollection matchCollection2 = regex1.Matches(group.Value);
                                    ++index2;
                                    group.Value.Replace("'", "''");
                                    data.allGroups[index2] = index2 != 8 ? group.Value.Replace("'", "''") : group.Value.Replace("'", "''") + str4;
                                    if (group.Name == "1")
                                    {
                                        str3 = matchCollection2[0].Value;
                                        data.allGroups[0] = matchCollection2[0].Value;
                                        if (str3 == str1)
                                            ++num1;
                                        else
                                            num1 = 0;
                                    }
                                    if (group.Name == (current.Groups.Count - 1).ToString())
                                    {
                                        bool flag2 = false;
                                        while (!flag2)
                                        {
                                            try
                                            {
                                                string str5 = str3 + "-" + num1.ToString();
                                                dictionary.Add(str5, data);
                                                int count = dictionary.Count;
                                                if (this.writeData(str5, data, project))
                                                    flag1 = true;
                                                str1 = str3;
                                                flag2 = true;
                                            }
                                            catch (ArgumentException ex)
                                            {
                                                ++num1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (enumerator is IDisposable disposable)
                            disposable.Dispose();
                    }
                }
            }
            this.addLogToProject(this.pKey);
            loggerProgressBar.Visible = false;
            if (!flag1)
                this.detachLogByID(project.ToString());
            Project.log.Info((object)string.Format("Records Processed {0}", (object)num2));
            Project.log.Info((object)string.Format("Records Skipped {0}", (object)num3));
            Project.log.Info((object)string.Format("Records read {0}", (object)index1));
            Project.log.Info((object)string.Format("Records Extended {0}", (object)num4));
        }

        public bool writeData(string recKey, dataLine data, int logID)
        {
            if (data.allGroups[6].IndexOf("]") != data.allGroups[6].LastIndexOf("]"))
            {
                int num = data.allGroups[6].IndexOf("]") + 1;
                data.allGroups[8] = data.allGroups[6].Substring(num, data.allGroups[6].Length - num) + data.allGroups[7] + data.allGroups[8];
                data.allGroups[6] = data.allGroups[6].Substring(0, num);
                data.allGroups[7] = "";
            }
            if (data.allGroups[7] != "" && data.allGroups[7].IndexOf("]") != data.allGroups[7].LastIndexOf("]"))
            {
                int num = data.allGroups[7].IndexOf("]") + 1;
                data.allGroups[8] = data.allGroups[7].Substring(num, data.allGroups[7].Length - num) + data.allGroups[8];
                data.allGroups[7] = data.allGroups[7].Substring(0, num);
            }
            if (data.allGroups[1].Length > 100 || data.allGroups[2].Length > 100 || data.allGroups[3].Length > 100 || data.allGroups[4].Length > 100 || data.allGroups[5].Length > 100 || data.allGroups[7].Length > 100)
                Console.WriteLine("error");
            return new DbCrud().crudToDb("INSERT INTO loginfo(logkey, group1, group2, group3, group4, group5, group6, group7, group8,prjKey, logID) \r\n                        VALUES('" + recKey + "','" + data.allGroups[1] + "','" + data.allGroups[2] + "','" + data.allGroups[3] + "','" + data.allGroups[4] + "','" + data.allGroups[5] + "','" + data.allGroups[6] + "','" + data.allGroups[7] + "','" + WebUtility.HtmlEncode(data.allGroups[8] + data.allGroups[9]) + "','" + this.Key + "','" + logID.ToString() + "')");
        }

        public bool writeLogDetail(string recKey, string data, int logID) => new DbCrud().crudToDb("INSERT INTO logDetail(logkey,detailInfo,prjKey, logID) \r\n                        VALUES('" + recKey + "','" + data + "','" + this.Key + "','" + logID.ToString() + "')");

        public string ReadFile(string fileName, string path) => System.IO.File.ReadAllText(path + fileName);

        public void getData(string regExStr, string recordType, string logID, int option)
        {
            Dictionary<string, string> dictionary = this.readData("SELECT logkey,id,group8 from [loginfo] WHERE group8 like '" + regExStr + "' AND logID =" + logID);
            if (dictionary == null)
                return;
            List<typeRec> typeRecs = new List<typeRec>();
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                string[] strArray = keyValuePair.Value.Split('\u001C');
                string recordType1 = App.Prj.RecordTypes[option, 2];
                if ((!(recordType == "1") && !(recordType.Substring(0, 1) == "3") || strArray.Length >= 4) && (!(recordType1 != "") || !(recordType1 != strArray[Convert.ToInt32(App.Prj.RecordTypes[option, 1])].Substring(0, App.Prj.RecordTypes[option, 2].Length))))
                    typeRecs.Add(new typeRec()
                    {
                        typeIndex = keyValuePair.Key,
                        typeContent = keyValuePair.Value
                    });
            }
            IMessage record = LoggerFactory.Create_Record(recordType);
            if (record == null)
                return;
            if (record.writeData(typeRecs, this.Key, logID))
                this.setBitToTrue(recordType, logID);
            else
                this.getLoggerProgressBar().Visible = false;
        }

        private void setBitToTrue(string recordType, string logID)
        {
            if (new DbCrud().crudToDb("UPDATE logs SET " + this.recTypesDic[recordType] + " = 1 WHERE id = " + logID))
                ;
        }

        public async Task<DataTable> getGroupOptionsAsync(string logID, string fieldName)
        {
            DataTable dataTable = new DataTable();
            DbCrud crud = new DbCrud();

            dataTable = await crud.GetTableFromDbAsync("SELECT DISTINCT " + fieldName + " FROM loginfo WHERE logID =" + logID + " ORDER BY " + fieldName + " ASC");
            return dataTable;
        }


        public DataTable getGroupOptions(string logID, string fieldName)
        {
            DataTable dataTable = new DataTable();
            // return dataTable;
            return new DbCrud().GetTableFromDb("SELECT DISTINCT " + fieldName + " FROM loginfo WHERE logID =" + logID + " ORDER BY " + fieldName + " ASC");
        }

        public DataTable getALogByIDWithCriteria(
          string logID,
          string columnName,
          string sqlLike)
        {
            DataTable dataTable = new DataTable();

            string sql = @"SELECT [id],[logkey],[group1] as 'Timestamp'," +
                "                           [group2] as 'Log Level',[group3] as 'File Name'," +
                "                           [group4] as 'Class',[group5] as 'Method'," +
                "                           [group6] as 'Type'," +
                "                           [group7] as 'Log'," +
                "                           [group8] as 'Log Data',[group9]," +
                "                           [prjKey],[logID] FROM [loginfo] " +
                "                            WHERE logID =" + logID + " AND " + sqlLike + "' order by id asc";
            //            "                            WHERE logID =" + logID + " AND " + sqlLike.Substring(0, sqlLike.LastIndexOf("%") + 1) + "' order by id asc";

            DataTable tableFromDb = new DbCrud().GetTableFromDb(sql);
            if (tableFromDb != null)
            {
                foreach (DataRow row in (InternalDataCollectionBase)tableFromDb.Rows)
                    row[9] = (object)WebUtility.HtmlDecode(row[9].ToString());
            }
            return tableFromDb;
        }

        public DataTable getALogByIDWithRegExp(string logID, string sqlLike, string regExpStr)
        {
            DataTable dataTable = new DataTable();
            DataTable alogByIdWithRegExp = new DataTable();
            DataTable tableFromDb = new DbCrud().GetTableFromDb("SELECT [id],[logkey],[group1] as 'Timestamp'," +
                "                            [group2] as 'Log Level',[group3] as 'File Name'," +
                "                            [group4] as 'Class',[group5] as 'Method'," +
                "                            [group6] as 'Type'," +
                "                            [group7] as 'Log'," +
                "                            [group8] as 'Log Data',[group9]," +
                "                            [prjKey],[logID] FROM [loginfo] " +
                "                            WHERE logID =" + logID + " AND " + sqlLike + " order by id asc");
            Regex regex = new Regex(regExpStr);
            if (tableFromDb != null)
            {
                alogByIdWithRegExp = tableFromDb.Clone();
                foreach (DataRow row1 in tableFromDb.Rows)
                {
                    row1[9] = (object)WebUtility.HtmlDecode(row1[9].ToString());
                    if ((uint)regex.Matches(row1[9].ToString()).Count > 0U)
                    {
                        DataRow row2 = alogByIdWithRegExp.NewRow();
                        row2.ItemArray = row1.ItemArray;
                        alogByIdWithRegExp.Rows.Add(row2);
                    }
                }
            }
            return alogByIdWithRegExp;
        }

        //public async Task<DataTable> getALogByIDWithRegExpAsync (string logID, string sqlLike, string regExpStr)
        //{
        //    DataTable dataTable = new DataTable();
        //    DataTable alogByIdWithRegExp = new DataTable();
        //    DataTable tableFromDb = new DbCrud().GetTableFromDb("SELECT [id],[logkey],[group1] as 'Timestamp'," +
        //        "                            [group2] as 'Log Level',[group3] as 'File Name'," +
        //        "                            [group4] as 'Class',[group5] as 'Method'," +
        //        "                            [group6] as 'Type'," +
        //        "                            [group7] as 'Log'," +
        //        "                            [group8] as 'Log Data',[group9]," +
        //        "                            [prjKey],[logID] FROM [loginfo] " +
        //        "                            WHERE logID =" + logID + " AND " + sqlLike + " order by id asc");
        //    Regex regex = new Regex(regExpStr);
        //    if (tableFromDb != null)
        //    {
        //        alogByIdWithRegExp = tableFromDb.Clone();
        //        foreach (DataRow row1 in tableFromDb.Rows)
        //        {
        //            row1[9] = (object)WebUtility.HtmlDecode(row1[9].ToString());
        //            if ((uint)regex.Matches(row1[9].ToString()).Count > 0U)
        //            {
        //                DataRow row2 = alogByIdWithRegExp.NewRow();
        //                row2.ItemArray = row1.ItemArray;
        //                alogByIdWithRegExp.Rows.Add(row2);
        //            }
        //        }
        //    }
        //    return alogByIdWithRegExp;
        //}



        public DataTable getALogByIDWithCriteria2(
          string logID,
          string columnName,
          string sqlLike)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = new DataTable();
            int index = 26;
            string recordType1 = App.Prj.RecordTypes[index, 0];
            string recordType2 = App.Prj.RecordTypes[index, 3];
            DataTable tableFromDb = new DbCrud().GetTableFromDb("SELECT [id],[logkey],[group1] as 'Timestamp', " +
                "[group2] as 'Log Level',[group3] as 'File Name'," +
                "[group4] as 'Class',[group5] as 'Method'," +
                "[group6] as 'Type'," +
                "[group7] as 'Log'," +
                "[group8] as 'Log Data',[group9]," +
                "[prjKey],[logID] FROM [loginfo] " +
                "WHERE logID =" + logID + " AND " + columnName + " like '" + recordType1 + "%' order by id asc");

            if (tableFromDb == null)
                return (DataTable)null;
            DataTable byIdWithCriteria2 = tableFromDb.Clone();
            foreach (DataRow row1 in (InternalDataCollectionBase)tableFromDb.Rows)
            {
                string[] strArray = WebUtility.HtmlDecode(row1["Log Data"].ToString()).Split('\u001C');
                string recordType3 = App.Prj.RecordTypes[index, 2];
                if ((!(recordType2 == "1") && !(recordType2.Substring(0, 1) == "3") || strArray.Length >= 4) && (!(recordType3 != "") || !(recordType3 != strArray[Convert.ToInt32(App.Prj.RecordTypes[index, 1])].Substring(0, App.Prj.RecordTypes[index, 2].Length))))
                {
                    DataRow row2 = byIdWithCriteria2.NewRow();
                    row2.ItemArray = row1.ItemArray;
                    byIdWithCriteria2.Rows.Add(row2);
                }
            }
            return byIdWithCriteria2;
        }

        public DataTable getALogByID(string logID)
        {
            DataTable dataTable = new DataTable();
            DataTable tableFromDb = new DbCrud().GetTableFromDb("SELECT [id],[logkey],[group1] as 'Timestamp',\r\n                                                              [group2] as 'Log Level',[group3] as 'File Name',\r\n                                                              [group4] as 'Class',[group5] as 'Method',\r\n                                                              [group6] as 'Type',\r\n                                                              [group7] as 'Log',\r\n                                                              [group8] as 'Log Data',[group9],\r\n                                                              [prjKey],[logID] FROM [loginfo] \r\n                                                              WHERE logID =" + logID + " order by id asc");
            if (tableFromDb != null)
            {
                foreach (DataRow row in (InternalDataCollectionBase)tableFromDb.Rows)
                    row[9] = (object)WebUtility.HtmlDecode(row[9].ToString());
            }
            return tableFromDb;
        }

        public DataTable getALogByID1000(string logID)
        {
            DataTable dataTable = new DataTable();
            DataTable tableFromDb = new DbCrud().GetTableFromDb("SELECT [id],[logkey],[group1] as 'Timestamp',\r\n                                                              [group2] as 'Log Level',[group3] as 'File Name',\r\n                                                              [group4] as 'Class',[group5] as 'Method',\r\n                                                              [group6] as 'Type',\r\n                                                              [group7] as 'Log',\r\n                                                              [group8] as 'Log Data',[group9],\r\n                                                              [prjKey],[logID] FROM [loginfo] \r\n                                                              WHERE logID =" + logID + " order by id asc limit 1000");
            if (tableFromDb != null)
            {
                foreach (DataRow row in (InternalDataCollectionBase)tableFromDb.Rows)
                    row[9] = (object)WebUtility.HtmlDecode(row[9].ToString());
            }
            return tableFromDb;
        }
        public async Task<DataTable> getALogByIDAsync(string logID)
        {

            DbCrud dbCrud = new DbCrud();

            DataTable tableFromDb = await dbCrud.GetTableFromDbAsync("SELECT [id],[logkey],[group1] as 'Timestamp',\r\n                                                              [group2] as 'Log Level',[group3] as 'File Name',\r\n                                                              [group4] as 'Class',[group5] as 'Method',\r\n                                                              [group6] as 'Type',\r\n                                                              [group7] as 'Log',\r\n                                                              [group8] as 'Log Data',[group9],\r\n                                                              [prjKey],[logID] FROM [loginfo] \r\n                                                              WHERE logID =" + logID + " order by id asc");
            if (tableFromDb != null)
            {
                foreach (DataRow row in (InternalDataCollectionBase)tableFromDb.Rows)
                    row[9] = (object)WebUtility.HtmlDecode(row[9].ToString());
            }
            return tableFromDb;
        }

        public DataTable getAllLogs(string prjID)
        {
            DataTable dataTable = new DataTable();
            return new DbCrud().GetTableFromDb("SELECT * FROM [logs] WHERE prjKey = '" + prjID + "' AND uploaded = 1");
        }

        public string getLogName(string prjID, string logID) => new DbCrud().GetScalarStrFromDb("SELECT [logFile] FROM [logs] WHERE prjKey = '" + prjID + "' AND id = '" + logID + "' AND uploaded = 1");

        public string getLogName(string logID) => new DbCrud().GetScalarStrFromDb("SELECT [logFile] FROM [logs] WHERE id = '" + logID + "' AND uploaded = 1");


        public Dictionary<string, int> showRecordBits(string logID)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            DbCrud dbCrud = new DbCrud();
            string sql1 = "SELECT COUNT(2) FROM screeninfo WHERE logID =" + logID;
            int scalarIntFromDb1 = dbCrud.GetScalarIntFromDb(sql1);
            dictionary.Add("Screens", scalarIntFromDb1);
            string sql2 = "SELECT COUNT(2) FROM stateinfo  WHERE logID =" + logID;
            int scalarIntFromDb2 = dbCrud.GetScalarIntFromDb(sql2);
            dictionary.Add("States", scalarIntFromDb2);
            string sql3 = "SELECT COUNT(2) FROM configParamsInfo  WHERE logID =" + logID;
            int scalarIntFromDb3 = dbCrud.GetScalarIntFromDb(sql3);
            dictionary.Add("Config Parameters", scalarIntFromDb3);
            string sql4 = "SELECT COUNT(2) FROM fitinfo  WHERE logID =" + logID;
            int scalarIntFromDb4 = dbCrud.GetScalarIntFromDb(sql4);
            dictionary.Add("FIT", scalarIntFromDb4);
            string sql5 = "SELECT COUNT(2) FROM configId  WHERE logID =" + logID;
            int scalarIntFromDb5 = dbCrud.GetScalarIntFromDb(sql5);
            dictionary.Add("Configuration ID", scalarIntFromDb5);
            string sql6 = "SELECT COUNT(2) FROM enhancedParamsInfo  WHERE logID =" + logID;
            int scalarIntFromDb6 = dbCrud.GetScalarIntFromDb(sql6);
            dictionary.Add("Enhanced Parameters", scalarIntFromDb6);
            string sql7 = "SELECT COUNT(2) FROM dateTime  WHERE logID =" + logID;
            int scalarIntFromDb7 = dbCrud.GetScalarIntFromDb(sql7);
            dictionary.Add("Date and Time", scalarIntFromDb7);
            string sql8 = "SELECT COUNT(2) FROM treq  WHERE logID =" + logID;
            int scalarIntFromDb8 = dbCrud.GetScalarIntFromDb(sql8);
            dictionary.Add("Transaction Request", scalarIntFromDb8);
            string sql9 = "SELECT COUNT(2) FROM treply  WHERE logID =" + logID;
            int scalarIntFromDb9 = dbCrud.GetScalarIntFromDb(sql9);
            dictionary.Add("Transaction Reply", scalarIntFromDb9);
            string sql10 = "SELECT COUNT(2) FROM iccCurrencyDOT  WHERE logID =" + logID;
            int scalarIntFromDb10 = dbCrud.GetScalarIntFromDb(sql10);
            dictionary.Add("ICC Currencies DOT", scalarIntFromDb10);
            string sql11 = "SELECT COUNT(2) FROM iccTransactionDOT  WHERE logID =" + logID;
            int scalarIntFromDb11 = dbCrud.GetScalarIntFromDb(sql11);
            dictionary.Add("ICC Transaction DOT", scalarIntFromDb11);
            string sql12 = "SELECT COUNT(2) FROM iccLanguageSupportT  WHERE logID =" + logID;
            int scalarIntFromDb12 = dbCrud.GetScalarIntFromDb(sql12);
            dictionary.Add("ICC Language Support", scalarIntFromDb12);
            string sql13 = "SELECT COUNT(2) FROM iccTerminalDOT  WHERE logID =" + logID;
            int scalarIntFromDb13 = dbCrud.GetScalarIntFromDb(sql13);
            dictionary.Add("ICC Terminal DOT", scalarIntFromDb13);
            string sql14 = "SELECT COUNT(2) FROM iccApplicationIDT  WHERE logID =" + logID;
            int scalarIntFromDb14 = dbCrud.GetScalarIntFromDb(sql14);
            dictionary.Add("ICC Terminal Acceptables AIDs Table", scalarIntFromDb14);
            string sql15 = "SELECT (SELECT COUNT(2) FROM solicitedStatus8  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatus9  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusB  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusC  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusF1  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusF2  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusF3  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusF4  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusF5  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusF6  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusF7  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusFH  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusFI  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusFJ  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusFK  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusFL  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusFM  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM solicitedStatusFN  WHERE logID =" + logID + ") ";
            int scalarIntFromDb15 = dbCrud.GetScalarIntFromDb(sql15);
            dictionary.Add("Solicited Status", scalarIntFromDb15);
            string sql16 = "SELECT (SELECT COUNT(2) FROM unsolicitedStatus5c  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatus61  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatus66  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatus71  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusA  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusB  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusC  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusD  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusE  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusF  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusG  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusH  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusK  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusL  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusM  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusP  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusQ  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusR  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusS  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusV  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatus77  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM unsolicitedStatusy  WHERE logID =" + logID + ") ";
            int scalarIntFromDb16 = dbCrud.GetScalarIntFromDb(sql16);
            dictionary.Add("Unsolicited Status", scalarIntFromDb16);
            string sql17 = "SELECT (SELECT COUNT(2) FROM encryptorInitData1  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM encryptorInitData2  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM encryptorInitData3  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM encryptorInitData4  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM encryptorInitData6  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM encryptorInitData7  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM encryptorInitData8  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM encryptorInitData9  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM encryptorInitDataA  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM encryptorInitDataB  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM encryptorInitDataD  WHERE logID =" + logID + ") +          (SELECT COUNT(2) FROM encryptorInitDataE  WHERE logID =" + logID + ") ";
            int scalarIntFromDb17 = dbCrud.GetScalarIntFromDb(sql17);
            dictionary.Add("Encryptor Initialization Data", scalarIntFromDb17);
            string sql18 = "SELECT COUNT(2) FROM uploadEjData  WHERE logID =" + logID;
            int scalarIntFromDb18 = dbCrud.GetScalarIntFromDb(sql18);
            dictionary.Add("Upload EJ Data", scalarIntFromDb18);
            string sql19 = "SELECT COUNT(2) FROM ackEjUploadBlock  WHERE logID =" + logID;
            int scalarIntFromDb19 = dbCrud.GetScalarIntFromDb(sql19);
            dictionary.Add("Ack EJ Upload Block", scalarIntFromDb19);
            string sql20 = "SELECT COUNT(2) FROM ackStopEj  WHERE logID =" + logID;
            int scalarIntFromDb20 = dbCrud.GetScalarIntFromDb(sql20);
            dictionary.Add("Ack EJ Stop", scalarIntFromDb20);
            string sql21 = "SELECT COUNT(2) FROM ejOptionsTimers  WHERE logID =" + logID;
            int scalarIntFromDb21 = dbCrud.GetScalarIntFromDb(sql21);
            dictionary.Add("EJ Options Timers", scalarIntFromDb21);
            string sql22 = "SELECT COUNT(2) FROM extEncryption  WHERE logID =" + logID;
            int scalarIntFromDb22 = dbCrud.GetScalarIntFromDb(sql22);
            dictionary.Add("Extended Encryption Key Change", scalarIntFromDb22);
            string sql23 = "SELECT COUNT(2) FROM terminalCommands  WHERE logID =" + logID;
            int scalarIntFromDb23 = dbCrud.GetScalarIntFromDb(sql23);
            dictionary.Add("Terminal Commands", scalarIntFromDb23);
            string sql24 = "SELECT COUNT(2) FROM macFieldSelection  WHERE logID =" + logID;
            int scalarIntFromDb24 = dbCrud.GetScalarIntFromDb(sql24);
            dictionary.Add("MAC Field Selection", scalarIntFromDb24);
            string sql25 = "SELECT COUNT(2) FROM dispenserMapping  WHERE logID =" + logID;
            int scalarIntFromDb25 = dbCrud.GetScalarIntFromDb(sql25);
            dictionary.Add("Dispenser Mapping", scalarIntFromDb25);
            string sql26 = "SELECT COUNT(2) FROM interactiveTranResponse  WHERE logID =" + logID;
            int scalarIntFromDb26 = dbCrud.GetScalarIntFromDb(sql26);
            dictionary.Add("Interactive Transaction Response", scalarIntFromDb26);
            string sql27 = "SELECT COUNT(2) FROM voiceGuidance  WHERE logID =" + logID;
            int scalarIntFromDb27 = dbCrud.GetScalarIntFromDb(sql27);
            dictionary.Add("Voice Guidance", scalarIntFromDb27);

            return dictionary;
        }

        public new Dictionary<string, string> readData(string sql)
        {
            DataTable dataTable = new DataTable();
            DataTable tableFromDb = new DbCrud().GetTableFromDb(sql);
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (tableFromDb != null)
            {
                foreach (DataRow row in (InternalDataCollectionBase)tableFromDb.Rows)
                    dictionary.Add(row[0].ToString() + Convert.ToInt32(row[1]).ToString(), WebUtility.HtmlDecode(row[2].ToString()));
            }
            return dictionary;
        }

        public void detachLogByID(string logID)
        {
            if (new DbCrud().crudToDb("DELETE from loginfo WHERE logID = " + logID +
                                      " ;DELETE from logs WHERE ID = " + logID +
                                      " ;UPDATE Project SET prjlogs = prjlogs - 1 " +
                                      "WHERE prjlogs > 0 and prjKey ='" + App.Prj.Key + "'"))
            {
                dropDataByLogID(logID);
            }
        }

        internal void dropDataByLogID(string logID)
        {
            string sql = $@"delete from [configId] WHERE logID = {logID};
            delete from [configParamsInfo] WHERE logID = {logID};
            delete from [configParamsTimers] WHERE logID = {logID};
            delete from [enhancedParams] WHERE logID = {logID};
            delete from [enhancedParamsInfo] WHERE logID = {logID};
            delete from [enhancedTimers] WHERE logID = {logID};
            delete from [extEncryption] WHERE logID = {logID};
            delete from [macFieldSelection] WHERE logID = {logID};
            delete from [fitinfo] WHERE logID = {logID};
            delete from [stateinfo] WHERE logID = {logID};
            delete from [screeninfo] WHERE logID = {logID};
            delete from treply WHERE logID = {logID};
            delete from tReplyCheckProcessing WHERE logID = {logID};
            delete from treplyPrinterData WHERE logID = {logID};
            delete from treq WHERE logID = {logID};
            delete from treqOptions WHERE logID = {logID};
            delete from treqCurrencies WHERE logID = {logID};
            delete from treqChecks WHERE logID = {logID};
            DELETE FROM [EMVConfiguration] WHERE logID = {logID};
            DELETE FROM [ICCCurrencyDOT] WHERE logID = {logID};
            DELETE FROM [ICCTransactionDOT] WHERE logID = {logID};
            DELETE FROM [ICCLanguageSupportT] WHERE logID = {logID};
            DELETE FROM [ICCTerminalDOT] WHERE logID = {logID};
            DELETE FROM [ICCApplicationIDT] WHERE logID = {logID};
            DELETE FROM [solicitedStatus9] WHERE logID = {logID};
            DELETE FROM [solicitedStatus8] WHERE logID = {logID};
            DELETE FROM [solicitedStatusB] WHERE logID = {logID};
            DELETE FROM [solicitedStatusC] WHERE logID = {logID};
            DELETE FROM [solicitedStatusF1] WHERE logID = {logID};
            DELETE FROM [solicitedStatusF2] WHERE logID = {logID};
            DELETE FROM [solicitedStatusF3] WHERE logID = {logID};
            DELETE FROM [solicitedStatusF4] WHERE logID = {logID};
            DELETE FROM [solicitedStatusF5] WHERE logID = {logID};
            DELETE FROM [solicitedStatusF6] WHERE logID = {logID};
            DELETE FROM [solicitedStatusF7] WHERE logID = {logID};
            DELETE FROM [solicitedStatusFH] WHERE logID = {logID};
            DELETE FROM [solicitedStatusFI] WHERE logID = {logID};
            DELETE FROM [solicitedStatusFJ] WHERE logID = {logID};
            DELETE FROM [solicitedStatusFK] WHERE logID = {logID};
            DELETE FROM [solicitedStatusFL] WHERE logID = {logID};
            DELETE FROM [solicitedStatusFM] WHERE logID = {logID};
            DELETE FROM [solicitedStatusFN] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusA] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusB] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusC] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusD] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusE] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusF] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusG] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusH] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusK] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusL] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusM] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusP] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusQ] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusR] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusS] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusV] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatus77] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatusY] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatus5c] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatus61] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatus66] WHERE logID = {logID};
            DELETE FROM [unsolicitedStatus71] WHERE logID = {logID};
            DELETE FROM [encryptorInitData1] WHERE logID = {logID};
            DELETE FROM [encryptorInitData2] WHERE logID = {logID};
            DELETE FROM [encryptorInitData3] WHERE logID = {logID};
            DELETE FROM [encryptorInitData4] WHERE logID = {logID};
            DELETE FROM [encryptorInitData6] WHERE logID = {logID};
            DELETE FROM [encryptorInitData7] WHERE logID = {logID};
            DELETE FROM [encryptorInitData8] WHERE logID = {logID};
            DELETE FROM [encryptorInitData9] WHERE logID = {logID};
            DELETE FROM [encryptorInitDataA] WHERE logID = {logID};
            DELETE FROM [encryptorInitDataB] WHERE logID = {logID};
            DELETE FROM [encryptorInitDataD] WHERE logID = {logID};
            DELETE FROM [encryptorInitDataE] WHERE logID = {logID};
            DELETE FROM [uploadEjData] WHERE logID = {logID};
            DELETE FROM [ackEjUploadBlock] WHERE logID = {logID};
            DELETE FROM [ackStopEj] WHERE logID = {logID};
            DELETE FROM [terminalCommands] WHERE logID = {logID};
            DELETE FROM [dispenserMapping] WHERE logID = {logID};
            DELETE FROM [interactiveTranResponse] WHERE logID = {logID};
            DELETE FROM [logDetail] WHERE logID = {logID};
            DELETE FROM [voiceGuidance] WHERE logID = {logID};
            DELETE FROM [generalInfo] WHERE logID = {logID};
            DELETE FROM [encryptorInitData5] WHERE logID = {logID};
            DELETE FROM [dateTime] WHERE logID = {logID};
            DELETE FROM [screenRec] WHERE logID = {logID};
            DELETE FROM [ejOptionsTimers] WHERE logID = {logID};
            DELETE FROM [staterec] WHERE logID = {logID};
            DELETE FROM [fitrec] WHERE logID = {logID}; ";


            //delete from[loginfo] WHERE logID = { logID };
            //delete from[project] WHERE logID = { logID };
            //delete from[logs] WHERE logID = { logID };

            DbCrud DB = new DbCrud();
            bool dropResult = DB.crudToDb(sql);
        }

        internal void ValidateUser(ToolStripMenuItem mi)
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                log.Debug($"Admin switch found");
                if (args[1].ToLower().Equals("/admin"))

                    mi.Visible = true;
                GetPassword psw = new GetPassword();
                psw.ShowDialog();
            }
            else
            {
                log.Debug($"Admin switch not found");
                mi.Visible = false;
            }
        }
    }
}
