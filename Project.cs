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
                                          };


        private List<StateRec> extensionsLst = new List<StateRec>();

        public List<StateRec> ExtensionsLst
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
            pKey = "";
            pName = "";
            pBrief = "";
            pLogs = 0;

            // mlh: New scans needs to be added here!

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
            recTypesDic.Add("81", "iccCurrencyDOT");
            recTypesDic.Add("82", "iccTransactionDOT");
            recTypesDic.Add("83", "iccLanguageSupportT");
            recTypesDic.Add("84", "iccTerminalDOT");
            recTypesDic.Add("85", "iccApplicationIDT");
            recTypesDic.Add("22", "solicitedStatus");
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

        public Dictionary<string, Project> getAllProjects()
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
                    this.pKey = row[1].ToString();
                    this.Name = row[2].ToString();
                    this.Brief = row[3].ToString();
                    this.pLogs = Convert.ToInt32(row[4]);
                    dicData.Add(row[1].ToString() + Convert.ToInt32(row[0]).ToString(), this);
                }
            }
            return dicData;
        }

        public Dictionary<string, Project> getProjectByName(string pName)
        {
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();
            string sql = @"SELECT * FROM project WHERE prjName ='" + pName + "'";
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
        /// Received the DataDescription record 
        /// Use a ENV tag finder on DataDescription table
        /// </summary>
        /// <returns></returns>
        //public string getEMVTags(string CAMContent)
        //{
        //    string camDataField = "";

        //    string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'X'  AND subRecType like '%'";

        //    DbCrud db = new DbCrud();
        //    DataTable dt = db.GetTableFromDb(sql);



        //    foreach (DataRow dr in dtDataDescription.Rows)
        //    {
        //        // split CAMContent and look for offset
        //        string[] contentFields = CAMContent.Split(' ');



        //        if (dr[2] == contentFields[1])
        //        {

        //        }
        //    }

        //    return null;
        //}

        public bool updateProjectByName(Project project, string pName, string pBrief)
        {
            string sql = @"UPDATE Project SET prjName ='" + pName +
                   "', prjBrief ='" + pBrief +
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

        public void uploadLog(string filename)
        {
            int logID = attachLogToProject(this.pKey, filename);

            Dictionary<string, dataLine> dicData = new Dictionary<string, dataLine>();

            WriteLog("c:", "test.txt", "Testing Testing");
            string[] lstLines = File.ReadAllLines(filename);

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
                    recSkipped++;
                    continue;
                }

                while ((lineProcess < lstLines.Length && lstLines[lineProcess].Length > 0
                    && lstLines[lineProcess].Substring(0, 1) != "["
                    && lstLines[lineProcess].Substring(0, 1) != "="
                    ) ||
                  (lineProcess < lstLines.Length && lstLines[lineProcess].Length == 0))
                {
                    if (lstLines[lineProcess] != "")
                    {
                        strLine += lstLines[lineProcess] + System.Environment.NewLine;
                    }
                    lineProcess++;
                    recExtent++;
                }

                if (strLine.EndsWith(System.Environment.NewLine))
                {
                    strLine = strLine.Substring(0, strLine.Length - 2);
                }

                if (strLine.Contains("2020-07-27 11:24:24-478"))
                                    { 
                }


                // 
                Regex openGroup9 = new Regex(@"(\[.*\])(\[.*\])(\[.*\])(\[.*\])(\[.*\])(\[.*\])(\[.*\])(\[.*\])?(.*)");
                Regex openGroup8 = new Regex(@"(\[.*\])(\[.*\])(\[.*\])(\[.*\])(\[.*\])(\[.*\])(\[.*\])?(.*)");
                Regex closeGroup = new Regex("]");
                Regex dateGroup = new Regex(@"\d\d\d\d-\d\d-\d\d.\d\d:\d\d:\d\d-\d\d\d");

                MatchCollection openGroupContent = openGroup9.Matches(strLine);
                if (openGroupContent.Count == 0)
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
                                    if (recKey == "2020-07-27 11:24:24-478")
                                    {

                                    }
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
            if (flagWriteData == false)
                detachLogByID(logID.ToString());

            log.Info($"Records Processed {recProcessed}");
            log.Info($"Records Skipped {recSkipped}");
            log.Info($"Records read {lineProcess}");
            log.Info($"Records Extended {recExtent}");
        }

        public bool writeData(string recKey, dataLine data, int logID)
        {
            String sql = "";

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

        public string ReadFile(string fileName, string path)
        {
            string fileToOpen = path + fileName;
            string lines = System.IO.File.ReadAllText(fileToOpen);
            return lines;
        }

        public void getData(string regExStr, string recordType, string logID)
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

                //if (recordType == "4" || recordType == "11" || recordType == "22")
                //{
                //    typeRec r = new typeRec();
                //    r.typeIndex = rec.Key;
                //    r.typeContent = rec.Value;
                //    typeList.Add(r);
                //    continue;
                //}

                //tmpTypes = rec.Value.Split((char)0x1c);

                //// EMV Configuration 81, 82, 83, 84, 85

                //if (recordType.Substring(0, 1) == "8" &&
                //    recordType.Substring(1, 1) == tmpTypes[2])
                //{
                //    typeRec r = new typeRec();
                //    r.typeIndex = rec.Key;
                //    r.typeContent = rec.Value;
                //    typeList.Add(r);
                //    continue;
                //}

                tmpTypes = rec.Value.Split((char)0x1c);

                if (recordType.Substring(0, 1) == "3" && 
                    recordType.Substring(1, 2) == tmpTypes[3])
                {
                    int myInd = tmpTypes[0].Length + tmpTypes[1].Length + tmpTypes[2].Length + tmpTypes[3].Length;
                    string typeData = rec.Value.Substring(myInd + 4, rec.Value.Length - (myInd + 4));

                    dataTypes = typeData.Split((char)0x1c);

                    foreach (string item in dataTypes)
                    {
                        typeRec r = new typeRec();
                        r.typeIndex = rec.Key;
                        r.typeContent = item;
                        typeList.Add(r);

                    }
                }
                else
                {
                    typeRec r = new typeRec();
                    r.typeIndex = rec.Key;
                    r.typeContent = rec.Value;
                    typeList.Add(r);
                }
            }

            IMessage theRecord = MessageFactory.Create_Record(recordType);

            if (theRecord != null)
            {
                if (theRecord.writeData(typeList, Key, logID))
                {
                    setBitToTrue(recordType, logID);
                }
            }
        }

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
                            [group6] as 'Type',[group7],
                            [group8] as 'Log Data',[group9],
                            [prjKey],[logID] FROM [loginfo] WHERE logID =" + logID +
                      " AND " + columnName + sqlLike;

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

        public DataTable getALogByID(string logID)
        {
            DataTable dt = new DataTable();

            string sql = @"SELECT [id],[logkey],[group1] as 'Timestamp',
                                                              [group2] as 'Log Level',[group3] as 'File Name',
                                                              [group4] as 'Class',[group5] as 'Method',
                                                              [group6] as 'Type',[group7],
                                                              [group8] as 'Log Data',[group9],
                                                              [prjKey],[logID] FROM [loginfo] 
                                                              WHERE logID =" + logID;

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

        // mlh Add record 

        public Dictionary<string, int> showRecordBits(string logID)
        {
            ///Return a Dictionary with type of record and amount

            Dictionary<string, int> dicBits = new Dictionary<string, int>();

            DbCrud db = new DbCrud();

            string sql = @"SELECT COUNT(*) FROM screeninfo WHERE logID =" + logID;
            int count = db.GetScalarIntFromDb(sql);
            dicBits.Add("screens", count);

            sql = @"SELECT COUNT(*) FROM stateinfo WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add("states", count);

            sql = @"SELECT COUNT(*) FROM configParamsInfo WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add("configParametersLoad", count);

            sql = @"SELECT COUNT(*) FROM fitinfo WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add("fit", count);

            sql = @"SELECT COUNT(*) FROM configId WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add("configID", count);

            sql = @"SELECT COUNT(*) FROM enhancedParamsInfo WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add("enhancedParametersLoad", count);

            sql = @"SELECT COUNT(*) FROM dateTime WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add("dateandtime", count);

            sql = @"SELECT COUNT(*) FROM treq WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add("treq", count);

            sql = @"SELECT COUNT(*) FROM treply WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add("treply", count);

            sql = @"SELECT COUNT(*) FROM iccCurrencyDOT WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add("iccCurrencyDOT", count);

            sql = @"SELECT COUNT(*) FROM iccTransactionDOT WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add("iccTransactionDOT", count);

            sql = @"SELECT COUNT(*) FROM iccLanguageSupportT WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add("iccLanguageSupportT", count);

            sql = @"SELECT COUNT(*) FROM iccTerminalDOT WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add("iccTerminalDOT", count);

            sql = @"SELECT COUNT(*) FROM iccApplicationIDT WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add("iccApplicationIDT", count);

            sql = @"SELECT COUNT(*) FROM solicitedStatus WHERE logID =" + logID;
            count = db.GetScalarIntFromDb(sql);
            dicBits.Add("solicitedStatus", count);
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
