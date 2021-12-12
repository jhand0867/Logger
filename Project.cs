using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string pKey;
        private string pName;
        private string pBrief;
        private int pLogs;

        public string Key   // property
        {
            get { return pKey; }   // get method
        }
        public string Name   // property
        {
            get { return pName; }   // get method
            set { pName = value; }  // set method
        }
        public string Brief   // property
        {
            get { return pBrief; }   // get method
            set { pBrief = value; }  // set method
        }
        public int Logs   // property
        {
            get { return pLogs; }   // get method
        }

        // mlh list type of messages

        /// <summary>
        /// Array of type of messages, the entry # is defined by optionselected for scanning
        /// column 1: Header of each message for regular expresion finding
        /// column 2: field index of Message subclass/Identifier
        /// column 3: value to compare
        /// column 4: recordtype 
        /// If message header match with the message, and the field index position in the message 
        /// matches the value to compare, return recordtype
        /// </summary>
        private readonly string[,] recordTypes = {
                                          { "ATM2HOST: 11", "0","", "11" },
                                          { "HOST2ATM: 4", "0","", "4" },
                                          { "HOST2ATM: 3", "3","11", "311" },
                                          { "HOST2ATM: 3", "3","12", "312" },
                                          { "HOST2ATM: 3", "3","13", "313" },
                                          { "HOST2ATM: 3", "3","15", "315" },
                                          { "HOST2ATM: 3", "3","16", "316" },
                                          { "HOST2ATM: 3", "3","1A", "31A" },
                                          { "HOST2ATM: 3", "3","1B", "31B" },
                                          { "HOST2ATM: 3", "3","1C", "31C" },
                                          { "HOST2ATM: 3", "3","1E", "31E" },
                                          { "HOST2ATM: 8", "2","1", "81" },
                                          { "HOST2ATM: 8", "2","2", "82" },
                                          { "HOST2ATM: 8", "2","3", "83" },
                                          { "HOST2ATM: 8", "2","4", "84" },
                                          { "HOST2ATM: 8", "2","5", "85" },
                                          { "ATM2HOST: 22", "0","", "22" },
                                          { "ATM2HOST: 12", "0","", "12" },
                                          { "ATM2HOST: 23", "0","", "23" },
                                          { "ATM2HOST: 61", "0","", "61H" },
                                          { "HOST2ATM: 6", "3","1", "61J" },
                                          { "HOST2ATM: 6", "3","2", "62" },
                                          { "HOST2ATM: 6", "3","3", "63" },
                                          { "HOST2ATM: 3", "3","4", "34" },
                                          { "HOST2ATM: 1", "0","", "1" },
                                          { "HOST2ATM: 3", "3","2", "32" },
// this ones are added for LogView selection 
                                          { "ATM2HOST: 22","3","F", "22" },
        };


        private List<StateData> extensionsLst = new List<StateData>();

        public List<StateData> ExtensionsLst
        {
            get { return extensionsLst; }
            set { extensionsLst = value; }
        }
        public string[,] RecordTypes
        {
            get { return recordTypes; }
        }

        public Dictionary<string, string> recTypesDic = new Dictionary<string, string>();


        /// <summary>
        /// Constructor setup Dictionary with recordtypes with its matching class name
        /// </summary>

        public Project()
        {
            log.Info($"Accessing Project Data");
            pKey = "";
            pName = "";
            pBrief = "";
            pLogs = 0;

            // mlh: New scans needs to be added here!
            // This name must match the name in the logs table
            // These names must match the value of the queryName
            // in the sqlBuilder table.

            recTypesDic.Add("11", "treq");
            recTypesDic.Add("4", "treply");
            recTypesDic.Add("311", "screens");
            recTypesDic.Add("312", "states");
            recTypesDic.Add("313", "configParametersLoad");
            recTypesDic.Add("315", "fit");
            recTypesDic.Add("316", "configID");
            recTypesDic.Add("31A", "enhancedParametersLoad");
            recTypesDic.Add("31B", "mac");
            recTypesDic.Add("31C", "dateandtime");
            recTypesDic.Add("31E", "dispenserCurrency");
            recTypesDic.Add("81", "iccCurrencyDOT");
            recTypesDic.Add("82", "iccTransactionDOT");
            recTypesDic.Add("83", "iccLanguageSupportT");
            recTypesDic.Add("84", "iccTerminalDOT");
            recTypesDic.Add("85", "iccApplicationIDT");
            recTypesDic.Add("22", "solicitedStatus");
            recTypesDic.Add("12", "unsolicitedStatus");
            recTypesDic.Add("23", "encryptorInitData");
            recTypesDic.Add("61H", "uploadEjData");
            recTypesDic.Add("61J", "ejAckBlock");
            recTypesDic.Add("62", "ejAckStop");
            recTypesDic.Add("63", "ejOptionsTimers");
            recTypesDic.Add("34", "extendedEncrypKeyChange");
            recTypesDic.Add("1", "terminalCommands");
            recTypesDic.Add("32", "interactiveTranResponse");
        }

        public Project(string pName, string pBrief)
        {
            createProject(pName, pBrief);
        }

        public Project getProjectByID(string prjID)
        {
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();
            string sql = @"SELECT * FROM project WHERE prjKey='" + prjID + "'";
            dt = db.GetTableFromDb(sql);

            Project pr = new Project();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    pr.pKey = row[1].ToString();
                    pr.Name = row[2].ToString();
                    pr.Brief = row[3].ToString();
                    pr.pLogs = Convert.ToInt32(row[4].ToString());
                }
            }
            return pr;
        }

        public DataTable getAllProjects()
        {
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();
            string sql = @"SELECT * FROM project";
            dt = db.GetTableFromDb(sql);

            return dt;
        }

        public Dictionary<string, Project> getAllProjects1()
        {
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();
            string sql = @"SELECT * FROM project";
            dt = db.GetTableFromDb(sql);

            Dictionary<string, Project> dicData = new Dictionary<string, Project>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Project prj = new Project();
                    prj.pKey = row[1].ToString();
                    prj.Name = row[2].ToString();
                    prj.Brief = row[3].ToString();
                    prj.pLogs = Convert.ToInt32(row[4]);
                    dicData.Add(row[1].ToString() + Convert.ToInt32(row[0]).ToString(), prj);
                }
            }
            return dicData;
        }

        public DataTable getProjectByName(string pName)
        {
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();
            string sql = @"SELECT * FROM project WHERE prjName ='" + pName + "'";
            return db.GetTableFromDb(sql);
        }


        public bool updateProjectByName(Project project, string pName, string pBrief)
        {
            string sql = @"UPDATE Project SET prjName ='" + pName + "', " +
                                             "prjBrief ='" + pBrief +
                   "' WHERE prjKey ='" + project.pKey + "'";

            DbCrud db = new DbCrud();
            if (db.crudToDb(sql) == false)
                return false;

            return true;
        }

        public bool addLogToProject(string pKey)
        {
            string sql = @"UPDATE Project SET prjLogs = prjLogs + 1 " +
                   "WHERE prjKey ='" + pKey + "' UPDATE Logs SET uploaded = 1";

            DbCrud db = new DbCrud();
            if (db.crudToDb(sql) == false)
                return false;

            return true;
        }

        public int attachLogToProject(string pKey, string pFilename)
        {
            string sql = @"INSERT INTO logs(prjKey, logFile, uploadDate) 
                    VALUES('" + pKey + "','" + pFilename + "', GETDATE()); SELECT CAST(scope_identity() AS int);";

            DbCrud db = new DbCrud();
            int newLogID = db.GetScalarIntFromDb(sql);
            return newLogID;
        }

        /// <summary>
        /// Add project to database 
        /// </summary>
        /// <param name="name">Name of project</param>
        /// <param name="brief">Shot Description</param>
        /// <returns>dicData 
        /// a dictionary with key project_id, 
        /// and value project object
        /// </returns>
        public Dictionary<string, Project> createProject(string name, string brief)
        {
            Name = name;
            Brief = brief;
            pLogs = 0;
            pKey = Guid.NewGuid().ToString();

            /**********/

            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();

            string sql = @"INSERT INTO Project(prjKey,prjName,prjBrief,prjLogs,createDate)" +
                   "VALUES('" + Key + "','" + Name + "','" + Brief + "','" + 0 + "',GETDATE()" + ")";

            dt = db.GetTableFromDb(sql);

            Dictionary<string, Project> dicData = new Dictionary<string, Project>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Project pr = new Project();
                    pr.pKey = row[1].ToString();
                    pr.Name = row[2].ToString();
                    pr.Brief = row[3].ToString();
                    pr.pLogs = Convert.ToInt32(row[4]);
                    dicData.Add(row[1].ToString() + Convert.ToInt32(row[0]).ToString(), pr);
                }
            }
            return dicData;
        }

        /// <summary>
        /// delete project given the project name
        /// </summary>
        /// <param name="prjName"></param>
        /// <returns>bool</returns>
        public bool deleteProjectByName(string prjName)
        {
            DbCrud db = new DbCrud();
            string sql;
            // need to obtain project ID
            sql = $"SELECT prjKey FROM project where prjName = '{prjName}'";
            string result = db.GetScalarStrFromDb(sql);

            sql = $"DELETE FROM loginfo WHERE prjKey = '{result}'";
            bool deleteLogInfo = db.crudToDb(sql);

            sql = $"DELETE project where prjName = '{prjName}'";
            bool deleteProjectInfo = db.crudToDb(sql);

            sql = $"DELETE logs where prjKey = '{result}'";
            bool deleteProjectLogs = db.crudToDb(sql);


            return deleteLogInfo && deleteProjectInfo && deleteProjectLogs;

        }

        private void deleteProjectLogsByID(string prjID)
        {
            string sql = $"DELETE loginfo where prjID = '{prjID}'";
            DbCrud db = new DbCrud();
            db.crudToDb(sql);
        }

        public DataTable getLogDetailByID(string logID)
        {
            string sql = $"SELECT * FROM logDetail WHERE logID = '{logID}'";
            DbCrud db = new DbCrud();
            return db.GetTableFromDb(sql);
        }

        public void uploadLog(string filename)
        {

            int logID = attachLogToProject(this.pKey, filename);

            Dictionary<string, dataLine> dicData = new Dictionary<string, dataLine>();

            WriteLog("c:", "test.txt", "Testing Testing");
            string[] lstLines = File.ReadAllLines(filename);

            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.Maximum = lstLines.Length + 1;
            lpb.LblTitle = this.ToString();

            int repLine = 0;
            int recProcessed = 0;
            int recSkipped = 0;
            int recExtent = 0;
            string prevTimeStamp = "";
            long lineProcess = 0;
            string strLine = null;
            bool flagWriteData = false;

            while (lineProcess < lstLines.Length)
            {
                strLine = lstLines[lineProcess];

                lineProcess++;

                if (strLine == "")
                {
                    recSkipped++;
                    continue;
                }
                if (strLine.Substring(0, 1) != "[")
                {
                    writeLogDetail("", strLine, logID);
                    continue;
                }

                Regex dateGroup = new Regex(@"\d\d\d\d-\d\d-\d\d.\d\d:\d\d:\d\d-\d\d\d");
                MatchCollection itsADate;

                while ((lineProcess < lstLines.Length && lstLines[lineProcess].Length > 0
                    && lstLines[lineProcess].Substring(0, 1) != "["
                    && lstLines[lineProcess].Substring(0, 1) != "="
                    ) ||
                  (lineProcess < lstLines.Length && lstLines[lineProcess].Length == 0) ||
                  (lineProcess < lstLines.Length && (itsADate = dateGroup.Matches(lstLines[lineProcess])).Count == 0))
                {

                    if (lstLines[lineProcess] != "")
                    {
                        strLine += lstLines[lineProcess] + System.Environment.NewLine;
                    }
                    lineProcess++;
                    recExtent++;
                }

                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                if (strLine.EndsWith(System.Environment.NewLine))
                {
                    strLine = strLine.Substring(0, strLine.Length - 2);
                }

                if (lineProcess == 44527 ||
                    lineProcess == 44528 ||
                    lineProcess == 44529)
                {

                }
                // 
                Regex openGroup9 = new Regex(@"(\[.*\])(\[.*\])(\[.*\])(\[.*\])(\[.*\])(\[.*\])(\[.*\])(\[.*\])?(.*)");
                Regex openGroup8 = new Regex(@"(\[.*\])(\[.*\])(\[.*\])(\[.*\])(\[.*\])(\[.*\])(\[.*\])?(.*)");

                //Regex openGroup9 = new Regex(@"(\[.*?\])(\[.*?\])(\[.*?\])(\[.*?\])(\[.*?\])(\[.*?\])(\[.*?\])(\[.*?\])?(.*)");
                //Regex openGroup8 = new Regex(@"(\[.*?\])(\[.*?\])(\[.*?\])(\[.*?\])(\[.*?\])(\[.*?\])(\[.*?\])?(.*)");
                Regex closeGroup = new Regex("]");


                MatchCollection openGroupContent = openGroup9.Matches(strLine);
                if ((openGroupContent.Count == 0) ||
                   (dateGroup.Matches(openGroupContent[0].Groups[0].ToString()).Count == 0))
                {
                    openGroupContent = openGroup8.Matches(strLine);
                }

                string strDate = "";
                dataLine record = new dataLine();
                record.allGroups = new string[11];

                foreach (Match item in openGroupContent[0].Groups)
                {
                    string strLineSub = "";

                    if (strLine.Length != item.Value.Length)
                    {
                        strLineSub = strLine.Substring(item.Value.Length, strLine.Length - item.Value.Length);
                        // recExtent = recExtent + strLineSub.Split('\n').Length;
                    }
                    int groupNumber = 0;
                    foreach (Group group in item.Groups)
                    {
                        if (group.Value + strLineSub == strLine)
                        {
                            recProcessed++;
                            continue;
                        }
                        MatchCollection matches = dateGroup.Matches(group.Value);

                        groupNumber++;
                        group.Value.Replace(@"'", @"''");

                        if (groupNumber == 8)
                        {
                            record.allGroups[groupNumber] = group.Value.Replace(@"'", @"''") + strLineSub;
                        }
                        else
                        {
                            record.allGroups[groupNumber] = group.Value.Replace(@"'", @"''");
                        }

                        if (group.Name == "1")
                        {
                            strDate = matches[0].Value;

                            record.allGroups[0] = matches[0].Value;

                            if (strDate == prevTimeStamp)
                            {
                                repLine++;
                            }
                            else
                            {
                                repLine = 0;
                            }
                        }

                        if (group.Name == (item.Groups.Count - 1).ToString())
                        {
                            bool flagAdd = false;

                            while (!flagAdd)
                            {
                                try
                                {
                                    // add the data to the dictionary
                                    //groupsData = groupsData + group.Value + strLineSub;

                                    string recKey = strDate + "-" + repLine.ToString();
                                    dicData.Add(recKey, record);


                                    int recCount = dicData.Count;
                                    // insert data into db
                                    if (writeData(recKey, record, logID))
                                    {
                                        flagWriteData = true;
                                    }

                                    prevTimeStamp = strDate;
                                    flagAdd = true;
                                }
                                catch (System.ArgumentException e)
                                {
                                    repLine++;
                                }
                            }
                        }
                    }
                    break;
                }
            }

            addLogToProject(this.pKey);
            lpb.Visible = false;
            //lpb.UpdateProgressBarEventHandler -= Lpb_UpdateProgressBarEventHandler;
            // lpb = null;

            if (flagWriteData == false)
                detachLogByID(logID.ToString());

            log.Info($"Records Processed {recProcessed}");
            log.Info($"Records Skipped {recSkipped}");
            log.Info($"Records read {lineProcess}");
            log.Info($"Records Extended {recExtent}");
        }

        //private void Lpb_UpdateProgressBarEventHandler(object sender, EventArgs e)
        //{          
        //    //LoggerProgressBar1.LoggerProgressBar1 lpb = sender as LoggerProgressBar1.LoggerProgressBar1;
        //    //lpb.ProgressBar1.Value = Convert.ToInt32(lpb.Value);
        //    //lpb.Percent1.Text = ((lpb.ProgressBar1.Value * 100) / lpb.Maximum).ToString() + "%";
        //    //lpb.Percent1.Refresh();   
        //}

        public bool writeData(string recKey, dataLine data, int logID)
        {
            String sql = "";

            // set groups straight

            if (data.allGroups[6].IndexOf("]") != data.allGroups[6].LastIndexOf("]"))
            {
                int indexGroup = data.allGroups[6].IndexOf("]") + 1;
                data.allGroups[8] = data.allGroups[6].Substring(indexGroup, data.allGroups[6].Length - indexGroup) +
                                     data.allGroups[7] + data.allGroups[8];
                data.allGroups[6] = data.allGroups[6].Substring(0, indexGroup);
                data.allGroups[7] = "";
            }

            if ((data.allGroups[7] != "") &&
                (data.allGroups[7].IndexOf("]") != data.allGroups[7].LastIndexOf("]")))
            {
                int indexGroup = data.allGroups[7].IndexOf("]") + 1;
                data.allGroups[8] = data.allGroups[7].Substring(indexGroup, data.allGroups[7].Length - indexGroup) +
                                    data.allGroups[8];
                data.allGroups[7] = data.allGroups[7].Substring(0, indexGroup);
            }

            if ((data.allGroups[1].Length > 100) ||
                (data.allGroups[2].Length > 100) ||
                (data.allGroups[3].Length > 100) ||
                (data.allGroups[4].Length > 100) ||
                (data.allGroups[5].Length > 100) ||
                (data.allGroups[7].Length > 100))
            {
                Console.WriteLine("error");
            }
            sql = @"INSERT INTO loginfo(logkey, group1, group2, group3, group4, group5, group6, group7, group8,prjKey, logID) 
                        VALUES('" + recKey + "','" +
                               data.allGroups[1] + "','" +
                               data.allGroups[2] + "','" +
                               data.allGroups[3] + "','" +
                               data.allGroups[4] + "','" +
                               data.allGroups[5] + "','" +
                               data.allGroups[6] + "','" +
                               data.allGroups[7] + "','" +
                               WebUtility.HtmlEncode(data.allGroups[8] + data.allGroups[9]) + "','" +
                               Key + "','" + logID + "')";

            DbCrud db = new DbCrud();
            if (db.crudToDb(sql) == false) { return false; };
            return true;
        }

        public bool writeLogDetail(string recKey, string data, int logID)
        {
            String sql = "";

            sql = @"INSERT INTO logDetail(logkey,detailInfo,prjKey, logID) 
                        VALUES('" + recKey + "','" +
                               data + "','" +
                               Key + "','" + logID + "')";

            DbCrud db = new DbCrud();
            if (db.crudToDb(sql) == false) { return false; };
            return true;
        }

        public string ReadFile(string fileName, string path)
        {
            string fileToOpen = path + fileName;
            string lines = System.IO.File.ReadAllText(fileToOpen);
            return lines;
        }

        // MLH To mimic or create function

        public void getData(string regExStr, string recordType, string logID, int option)
        {
            string sql = @"SELECT logkey,id,group8 from [logger].[dbo].[loginfo] " +
                          "WHERE group8 like '" + regExStr + "' " +
                          "AND logID =" + logID;
            Dictionary<string, string> data = readData(sql);

            if (data == null)
            {
                return;
            }

            string[] dataTypes = null;
            List<typeRec> typeList = new List<typeRec>();
            string[] tmpTypes;

            foreach (KeyValuePair<string, string> rec in data)
            {
                //// Request or Reply

                tmpTypes = rec.Value.Split((char)0x1c);

                string subRecType = App.Prj.RecordTypes[option, 2];

                // bypass messages that starts with  "HOST2ATM: 1" or ""HOST2ATM: 3" but do not have value in 
                // subfield 3 ie. [RECV]HOST2ATM: 170172376255118255139        [RECV]HOST2ATM: 34

                if (((recordType == "1") || (recordType.Substring(0, 1) == "3")) &&
                    (tmpTypes.Length < 4))
                {
                    continue;
                }

                if ((subRecType != "") &&
                     subRecType !=
                     tmpTypes[Convert.ToInt32(App.Prj.RecordTypes[option, 1])].Substring(0, App.Prj.RecordTypes[option, 2].Length))

                {
                    continue;
                }
                typeRec r = new typeRec();
                r.typeIndex = rec.Key;
                r.typeContent = rec.Value;
                typeList.Add(r);
            }

            IMessage theRecord = LoggerFactory.Create_Record(recordType);

            if (theRecord != null)
            {
                // mlh 
                if (theRecord.writeData(typeList, Key, logID))
                {
                    setBitToTrue(recordType, logID);
                }
                else
                {
                    LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
                    lpb.Visible = false;
                }
            }
        }

        //private LoggerProgressBar1.LoggerProgressBar1  getProgressBar()
        //{
        //    LoggerProgressBar1.LoggerProgressBar1 lpb = null;

        //    if (Application.OpenForms["ProjectData"].Controls.Find("LoggerProgressBar1", true).Length == 0)
        //    {
        //        lpb = new LoggerProgressBar1.LoggerProgressBar1();
        //        Application.OpenForms["ProjectData"].Controls.Add(lpb);
        //    }
        //    else
        //    {
        //        Control[] ca = Application.OpenForms["ProjectData"].Controls.Find("LoggerProgressBar1", true);
        //        lpb = (LoggerProgressBar1.LoggerProgressBar1)ca[0];
        //        lpb.Value = 0;
        //    }

        //    lpb.Dock = DockStyle.Bottom;
        //    lpb.Visible = true;
        //    lpb.LblTitle = this.ToString();

        //    return lpb;
        //}


        private void setBitToTrue(string recordType, string logID)
        {
            string recordTypeStr = recTypesDic[recordType];
            string sql = @"UPDATE logs SET " + recordTypeStr + " = 1 WHERE id = " + logID;

            DbCrud db = new DbCrud();
            if (db.crudToDb(sql) == false) { };
        }

        public DataTable getGroupOptions(string logID, string fieldName)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT DISTINCT " + fieldName + " FROM loginfo WHERE logID =" +
                                                                  logID + " ORDER BY " + fieldName + " ASC";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            return dt;
        }

        public DataTable getALogByIDWithCriteria(string logID, string columnName, string sqlLike)
        {
            DataTable dt = new DataTable();

            string sql = @"SELECT [id],[logkey],[group1] as 'Timestamp',
                            [group2] as 'Log Level',[group3] as 'File Name',
                            [group4] as 'Class',[group5] as 'Method',
                            [group6] as 'Type',
                            [group7] as 'Log',
                            [group8] as 'Log Data',[group9],
                            [prjKey],[logID] FROM [loginfo] WHERE logID =" + logID +
                      " AND " + columnName + sqlLike + " order by id asc";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row[9] = WebUtility.HtmlDecode(row[9].ToString());
                }
            }
            return dt;
        }




        public DataTable getALogByIDWithRegExp(string logID, string sqlLike, string regExpStr)
        {
            DataTable dt = new DataTable();
            DataTable dtout = new DataTable();
            DataRow dtrow;

            string sql = @"SELECT [id],[logkey],[group1] as 'Timestamp',
                            [group2] as 'Log Level',[group3] as 'File Name',
                            [group4] as 'Class',[group5] as 'Method',
                            [group6] as 'Type',
                            [group7] as 'Log',
                            [group8] as 'Log Data',[group9],
                            [prjKey],[logID] FROM [loginfo] WHERE logID =" + logID +
                       " AND " + sqlLike + " order by id asc";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);

            //#########/ /########

            // create the regexp for the specific search
            // - find the sqlbuilder - sqlDetail id
            // - read the sqlDetail records

            //Regex findReady9 = new Regex(@"ATM2HOST: 22\u001c.+\u001c\u001cF\u001c1");
            Regex findReady9 = new Regex(regExpStr);

            if (dt != null)
            {
                // here adding regexp
                dtout = dt.Clone();

                foreach (DataRow row in dt.Rows)
                {
                    row[9] = WebUtility.HtmlDecode(row[9].ToString());

                    MatchCollection findReady9Matches = findReady9.Matches(row[9].ToString());
                    if (findReady9Matches.Count != 0)
                    {
                        dtrow = dtout.NewRow();
                        dtrow.ItemArray = row.ItemArray;
                        // dtout.Rows.Add(['1','2','3','4','5','6','7','8','9','0','1','2','3']);
                        dtout.Rows.Add(dtrow);
                    }
                }
            }
            return dtout;
        }

        /// <summary>
        /// Nedd to add a new db table with the base fields for shortcuts data
        /// - title
        /// - index
        /// Build the dropdow menu using as input the records in the shortcuts table
        /// Dropdown to have one option per message name entered
        /// </summary>
        /// <param name="logID"></param>
        /// <param name="columnName"></param>
        /// <param name="sqlLike"></param>
        /// <returns></returns>

        public DataTable getALogByIDWithCriteria2(string logID, string columnName, string sqlLike)
        {
            DataTable dt = new DataTable();
            DataTable dtout = new DataTable();
            DataRow dtrow;

            string[] tmpTypes;
            int option = 26;

            string regExStr = App.Prj.RecordTypes[option, 0];
            string recordType = App.Prj.RecordTypes[option, 3];

            string sql = @"SELECT [id],[logkey],[group1] as 'Timestamp',
                            [group2] as 'Log Level',[group3] as 'File Name',
                            [group4] as 'Class',[group5] as 'Method',
                            [group6] as 'Type',
                            [group7] as 'Log',
                            [group8] as 'Log Data',[group9],
                            [prjKey],[logID] FROM [loginfo] WHERE logID =" + logID +
                      " AND " + columnName + " like '" + regExStr + "%' order by id asc";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);

            if (dt == null)
            {
                return null;
            }

            dtout = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                tmpTypes = WebUtility.HtmlDecode(row["Log Data"].ToString()).Split((char)0x1c);

                string subRecType = App.Prj.RecordTypes[option, 2];

                if (((recordType == "1") || (recordType.Substring(0, 1) == "3")) &&
                    (tmpTypes.Length < 4))
                {
                    continue;
                }

                if ((subRecType != "") &&
                     subRecType !=
                     tmpTypes[Convert.ToInt32(App.Prj.RecordTypes[option, 1])].Substring(0, App.Prj.RecordTypes[option, 2].Length))
                {
                    continue;
                }
                dtrow = dtout.NewRow();
                dtrow.ItemArray = row.ItemArray;
                dtout.Rows.Add(dtrow);
            }
            return dtout;
        }

        public DataTable getALogByID(string logID)
        {
            DataTable dt = new DataTable();

            string sql = @"SELECT [id],[logkey],[group1] as 'Timestamp',
                                                              [group2] as 'Log Level',[group3] as 'File Name',
                                                              [group4] as 'Class',[group5] as 'Method',
                                                              [group6] as 'Type',
                                                              [group7] as 'Log',
                                                              [group8] as 'Log Data',[group9],
                                                              [prjKey],[logID] FROM [loginfo] 
                                                              WHERE logID =" + logID + " order by id asc";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row[9] = WebUtility.HtmlDecode(row[9].ToString());
                }
            }
            return dt;
        }

        public DataTable getAllLogs(string prjID)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT * FROM [dbo].[logs] WHERE prjKey = '" + prjID + "' AND uploaded = 1";
            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            return dt;
        }

        public string getLogName(string prjID, string logID)
        {
            string result = "";
            string sql = @"SELECT [logFile] FROM [dbo].[logs] WHERE prjKey = '" + prjID + "' AND id = '" + logID + "' AND uploaded = 1";
            DbCrud db = new DbCrud();
            result = db.GetScalarStrFromDb(sql);
            return result;
        }

        // mlh Add record 

        public Dictionary<string, int> showRecordBits(string logID)
        {
            ///Return a Dictionary with type of record and amount

            Dictionary<string, int> dicBits = new Dictionary<string, int>();

            DbCrud db = new DbCrud();

            string sql = @"SELECT COUNT(2) FROM screeninfo WITH (NOLOCK) WHERE logID =" + logID;
            int count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["311"], count);

            sql = @"SELECT COUNT(2) FROM stateinfo WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);

            dicBits.Add(recTypesDic["312"], count);

            sql = @"SELECT COUNT(2) FROM configParamsInfo WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["313"], count);

            sql = @"SELECT COUNT(2) FROM fitinfo WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["315"], count);

            sql = @"SELECT COUNT(2) FROM configId WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["316"], count);

            sql = @"SELECT COUNT(2) FROM enhancedParamsInfo WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["31A"], count);

            sql = @"SELECT COUNT(2) FROM dateTime WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["31C"], count);

            sql = @"SELECT COUNT(2) FROM treq WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["11"], count);

            sql = @"SELECT COUNT(2) FROM treply WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["4"], count);

            sql = @"SELECT COUNT(2) FROM iccCurrencyDOT WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["81"], count);

            sql = @"SELECT COUNT(2) FROM iccTransactionDOT WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["82"], count);

            sql = @"SELECT COUNT(2) FROM iccLanguageSupportT WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["83"], count);

            sql = @"SELECT COUNT(2) FROM iccTerminalDOT WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["84"], count);

            sql = @"SELECT COUNT(2) FROM iccApplicationIDT WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["85"], count);

            sql = @"SELECT (SELECT COUNT(2) FROM solicitedStatus8 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatus9 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusB WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusC WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusF1 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusF2 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusF3 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusF4 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusF5 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusF6 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusF7 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusFH WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusFI WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusFJ WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusFK WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusFL WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusFM WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM solicitedStatusFN WITH (NOLOCK) WHERE logID =" + logID + ") ";
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["22"], count);

            sql = @"SELECT (SELECT COUNT(2) FROM unsolicitedStatus5c WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatus61 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatus66 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatus71 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusA WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusB WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusC WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusD WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusE WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusF WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusG WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusH WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusK WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusL WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusM WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusP WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusQ WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusR WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusS WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusV WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusw WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM unsolicitedStatusy WITH (NOLOCK) WHERE logID =" + logID + ") ";
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["12"], count);

            sql = @"SELECT (SELECT COUNT(2) FROM encryptorInitData1 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM encryptorInitData2 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM encryptorInitData3 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM encryptorInitData4 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM encryptorInitData6 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM encryptorInitData7 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM encryptorInitData8 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM encryptorInitData9 WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM encryptorInitDataA WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM encryptorInitDataB WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM encryptorInitDataD WITH (NOLOCK) WHERE logID =" + logID + ") +" +
                "          (SELECT COUNT(2) FROM encryptorInitDataE WITH (NOLOCK) WHERE logID =" + logID + ") ";

            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["23"], count);

            sql = @"SELECT COUNT(2) FROM uploadEjData WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["61H"], count);

            sql = @"SELECT COUNT(2) FROM ackEjUploadBlock WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["61J"], count);

            sql = @"SELECT COUNT(2) FROM ackStopEj WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["62"], count);

            sql = @"SELECT COUNT(2) FROM ejOptionsTimers WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["63"], count);

            sql = @"SELECT COUNT(2) FROM extEncryption WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["34"], count);

            sql = @"SELECT COUNT(2) FROM terminalCommands WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["1"], count);

            sql = @"SELECT COUNT(2) FROM macFieldSelection WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["31B"], count);

            sql = @"SELECT COUNT(2) FROM dispenserMapping WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["31E"], count);

            sql = @"SELECT COUNT(2) FROM interactiveTranResponse WITH (NOLOCK) WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add(recTypesDic["32"], count);

            return dicBits;
        }

        public new Dictionary<string, string> readData(string sql)
        {
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            Dictionary<string, string> dicData = new Dictionary<string, string>();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dicData.Add(row[0].ToString() + Convert.ToInt32(row[1]).ToString(), WebUtility.HtmlDecode(row[2].ToString()));
                    //                    dicData.Add(row[0].ToString() + Convert.ToInt32(row[1]).ToString(), row[2].ToString());
                }
            }
            return dicData;
        }

        public void detachLogByID(string logID)
        {
            string sql = @"DELETE from loginfo WHERE logID = " + logID +
                       " ;DELETE from logs WHERE ID = " + logID +
                       " ;UPDATE Project SET prjlogs = prjlogs - 1 " +
                       "WHERE prjlogs > 0 and prjKey ='" + App.Prj.Key + "'";
            DbCrud db = new DbCrud();
            if (db.crudToDb(sql) == false) { };
        }
    }
}
