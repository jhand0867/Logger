using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Logger
{
    public struct dataLine
    {
        public string group0;
        public string group1;
        public string group2;
        public string group3;
        public string group4;
        public string group5;
        public string group6;
        public string group7;
        public string group8;
        public string group9;
    }

    public struct typeRec
    {
        public string typeIndex;
        public string typeContent;
        public string typeAddData;
    }

    public class Project : App
    {
        public delegate void delegate1(typeRec typeRec, string str1, string str2);


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


        // Initialize all record types

        // mlh list type of messages

        //private string[] recordTypes = { "ATM2HOST: 11", "HOST2ATM: 4", "HOST2ATM: 8", "HOST2ATM: 3" };
        private readonly string[,] recordTypes = { { "ATM2HOST: 11", "0","", "00" }, 
                                          { "HOST2ATM: 4", "0","", "01" },
                                          { "HOST2ATM: 3", "3","11", "11" },
                                          { "HOST2ATM: 3", "3","12", "12" },
                                          { "HOST2ATM: 3", "3","13", "13" },
                                          { "HOST2ATM: 3", "3","15", "15" },
                                          { "HOST2ATM: 3", "3","16", "16" },
                                          { "HOST2ATM: 3", "3","1A", "1A" },
                                          { "HOST2ATM: 3", "3","1B", "1B" },
                                          { "HOST2ATM: 3", "3","1C", "1C" },
                                          { "HOST2ATM: 3", "3","1E", "1E" },
                                          { "HOST2ATM: 8", "2","1", "81" },
                                          { "HOST2ATM: 8", "2","2", "82" },
                                          { "HOST2ATM: 8", "2","3", "83" },
                                          { "HOST2ATM: 8", "2","4", "84" },
                                          { "HOST2ATM: 8", "2","5", "85" },
                                        };
        
        // to be used as index in tmptypes
        private int[] recordTypeIdx = { 0, 0, 2, 3 };
        private string[] subRecordTypes = { "81", "82", "83", "84", "85","311", "312", "313", "315", "316", "31A", "31B", "31C", "31E" };
        //private string[] subRecordTypes3 = { "11", "12", "13", "15", "16", "1A", "1B", "1C", "1E" };
        //private string[] subRecordTypes8 = { "1", "2", "3", "4", "5" };
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
        public int[] RecordTypeIdx
        {
            get { return recordTypeIdx; }
        }
        public string[] SubRecordTypes
        {
            get { return subRecordTypes; }
        }
        //public string[] SubRecordTypes3
        //{
        //    get { return subRecordTypes3; }
        //}
        //public string[] SubRecordTypes8
        //{
        //    get { return subRecordTypes8; }
        //}
        public Dictionary<string, string> recTypesDic = new Dictionary<string, string>();


        public Project()
        {
            // mlh why this is executed more than once

            pKey = "";
            pName = "";
            pBrief = "";
            pLogs = 0;

            // mlh: New scans needs to be added here!

            recTypesDic.Add("00", "treq");
            recTypesDic.Add("01", "treply"); 
            recTypesDic.Add("11", "screens");
            recTypesDic.Add("12", "states");
            recTypesDic.Add("13", "configParametersLoad");
            recTypesDic.Add("15", "fit");
            recTypesDic.Add("16", "configID");
            recTypesDic.Add("1A", "enhancedParametersLoad");
            recTypesDic.Add("1B", "mac");
            recTypesDic.Add("1C", "dateandtime");
            recTypesDic.Add("81", "iccCurrencyDOT");
            recTypesDic.Add("82", "iccTransactionDOT");
            recTypesDic.Add("83", "iccLanguageSupportT");
            recTypesDic.Add("84", "iccTerminalDOT");
            recTypesDic.Add("85", "iccApplicationIDT");
        }

        public Project(string pName, string pBrief)
        {
            createProject(pName, pBrief);
        }

        public Project getProjectByID(string prjID)
        {
            // here mlh

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
                    // pr.pLogs = Convert.ToInt32(dataReader.GetBoolean(4));
                }
            }
            return pr;
        }

        public Dictionary<string, Project> getAllProjects()
        {
            // here mlh

            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();
            string sql = @"SELECT * FROM project";
            dt = db.GetTableFromDb(sql);

            Dictionary<string, Project> dicData = new Dictionary<string, Project>();

            if (dt.Rows.Count > 0)
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
            // here mlh

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

        public bool updateProjectByName(Project project, string pName, string pBrief)
        {
                string sql = @"UPDATE Project SET prjName ='" + pName +
                       "', prjBrief ='" + pBrief +
                       "' WHERE prjKey ='" + project.pKey + "'";

                DbCrud db = new DbCrud();
                if (db.addToDb(sql) == false)
                    return false;

                return true;

        }

        public bool addLogToProject(string pKey)
        {
            string sql = @"UPDATE Project SET prjLogs = prjLogs + 1 " +
                   "WHERE prjKey ='" + pKey + "'";

            DbCrud db = new DbCrud();
            if (db.addToDb(sql) == false)
                return false;

            return true;

        }

        public int attachLogToProject(string pKey, string pFilename)
        {
            string sql = @"INSERT INTO logs(prjKey, logFile, uploadDate) 
                    VALUES('" + pKey + "','" + pFilename + "', GETDATE()); SELECT CAST(scope_identity() AS int);";
           
            DbCrud db = new DbCrud();
            int newLogID = db.GetScalarFromDb(sql);
            return newLogID;
        }

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

        public void uploadLog(string filename)
        {
            addLogToProject(this.pKey);
            int logID = attachLogToProject(this.pKey, filename);

            // create dictionary
            Dictionary<string, dataLine> dicData = new Dictionary<string, dataLine>();

            WriteLog("c:", "test.txt", "Testing Testing");
            string[] lstLines = File.ReadAllLines(filename);

            int repLine = 0;
            int recProcessed = 0;
            int recSkipped = 0;
            int readRecs = 0;
            int recExtent = 0;
            string prevTimeStamp = "";
            long lineProcess = 0;
            string strLine = null;

            //foreach (string strLine in lstLines)
            while (lineProcess < lstLines.Length)
            {
                strLine = lstLines[lineProcess];
                dataLine record = new dataLine();

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

                while (lineProcess < lstLines.Length && lstLines[lineProcess] != ""
                        && lstLines[lineProcess].Substring(0, 1) != "[")
                {
                    strLine += lstLines[lineProcess] + System.Environment.NewLine;
                    lineProcess++;
                }


                readRecs++;
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

                foreach (Match item in openGroupContent[0].Groups)
                {
                    string strLineSub = "";

                    if (strLine.Length != item.Value.Length)
                    {
                        strLineSub = strLine.Substring(item.Value.Length, strLine.Length - item.Value.Length);
                        recExtent = recExtent + strLineSub.Split('\n').Length;
                    }

                    foreach (Group group in item.Groups)
                    {
                        if (group.Value == strLine)
                        {
                            recProcessed++;
                            continue;
                        }
                        MatchCollection matches = dateGroup.Matches(group.Value);

                        group.Value.Replace(@"'", @"''");
                        switch (group.Name)
                        {
                            case "0":
                                strDate = matches[0].Value;
                                break;
                            case "1":
                                record.group1 = group.Value.Replace(@"'", @"''");
                                break;
                            case "2":
                                record.group2 = group.Value.Replace(@"'", @"''");
                                break;
                            case "3":
                                record.group3 = group.Value.Replace(@"'", @"''");
                                break;
                            case "4":
                                record.group4 = group.Value.Replace(@"'", @"''");
                                break;
                            case "5":
                                record.group5 = group.Value.Replace(@"'", @"''");
                                break;
                            case "6":
                                record.group6 = group.Value.Replace(@"'", @"''");
                                break;
                            case "7":
                                record.group7 = group.Value.Replace(@"'", @"''");
                                break;
                            case "8":
                                record.group8 = group.Value.Replace(@"'", @"''") + strLineSub;
                                break;
                            default:
                                record.group9 = group.Value.Replace(@"'", @"''");
                                break;
                        }

                        if (group.Name == "1")
                        {
                            strDate = matches[0].Value;

                            record.group0 = matches[0].Value;

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
                            //repLine = 0;
                            bool flagAdd = false;
                            while (!flagAdd)
                            {
                                try
                                {
                                    // add the data to the dictionary
                                    //groupsData = groupsData + group.Value + strLineSub;

                                    //dataLine rec = new dataLine();
                                    string recKey = strDate + "-" + repLine.ToString();
                                    dicData.Add(recKey, record);

                                    int recCount = dicData.Count;
                                    // insert data into db
                                    writeData(recKey, record, logID);


                                    prevTimeStamp = strDate;
                                    flagAdd = true;
                                    //repLine++;
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

            /*          Console.WriteLine("Records Processed {0}", recProcessed);
                        Console.WriteLine("Records Skipped {0}", recSkipped);
                        Console.WriteLine("Records read {0}", readRecs);
                        Console.WriteLine("Records Extended {0}", recExtent);
                        Console.ReadKey();*/


        }

        public void writeData(string recKey, dataLine data, int logID)
        {
                String sql = "";

                if ((data.group1.Length > 100) ||
                    (data.group2.Length > 100) ||
                    (data.group3.Length > 100) ||
                    (data.group4.Length > 100) ||
                    (data.group5.Length > 100) ||
                    (data.group7.Length > 100))
                {
                    Console.WriteLine("error");
                }


                sql = @"INSERT INTO loginfo(logkey, group1, group2, group3, group4, group5, group6, group7, group8,prjKey, logID) 
                        VALUES('" + recKey + "','" +
                                   data.group1 + "','" +
                                   data.group2 + "','" +
                                   data.group3 + "','" +
                                   data.group4 + "','" +
                                   data.group5 + "','" +
                                   data.group6 + "','" +
                                   data.group7 + "','" +
                                   WebUtility.HtmlEncode(data.group8 + data.group9) + "','" +
                                   Key + "','" + logID + "')";


                DbCrud db = new DbCrud();
                if (db.addToDb(sql) == false) { };

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
                // todo: check throw
                return;
            }

            string[] dataTypes = null;
            List<typeRec> typeList = new List<typeRec>();
            string[] tmpTypes;

            foreach (KeyValuePair<string, string> rec in data)
            {
                // tmpTypes = rec.Value.Split((char)0x1c);

                // Request or Reply

                if (recordType == "00" || recordType == "01")
                {
                    typeRec r = new typeRec();
                    r.typeIndex = rec.Key;
                    r.typeContent = rec.Value;
                    typeList.Add(r);
                    continue;
                }

                tmpTypes = rec.Value.Split((char)0x1c);

                // EMV Configuration 81, 82, 83, 84, 85

                if (recordType.Substring(0, 1) == "8" &&
                   ((recordType.Substring(1, 1) == "1" && tmpTypes[2] == "1") ||
                    (recordType.Substring(1, 1) == "2" && tmpTypes[2] == "2") ||
                    (recordType.Substring(1, 1) == "3" && tmpTypes[2] == "3") ||
                    (recordType.Substring(1, 1) == "4" && tmpTypes[2] == "4") ||
                    (recordType.Substring(1, 1) == "5" && tmpTypes[2] == "5")))
                {
                    typeRec r = new typeRec();
                    r.typeIndex = rec.Key;
                    r.typeContent = rec.Value;
                    typeList.Add(r);
                    continue;
                }

                if (tmpTypes[3] == recordType)
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
            }
            // jmh
            //public var processRecord =  new delegate1(new stateRec().writeData,){

            //}

            IMessage theRecord = MessageFactory.Create_Record(recordType);

            if (theRecord != null)
            {
                if (theRecord.writeData(typeList, Key, logID))
                {
                    setBitToTrue(recordType, logID);
                }
            }

            //switch (recordType)
            //{
            //    case "00":
            //        TRec tr = new TRec();
            //        if (tr.writeData(typeList, Key, logID))
            //        {
            //            setBitToTrue(recordType, logID);
            //        }

            //        break;
            //    case "01":
            //        TReply treply = new TReply();
            //        if (treply.writeData(typeList, Key, logID))
            //        {
            //            // set screen bit to true
            //            setBitToTrue(recordType, logID);
            //        }
            //        break;
            //    case "11":
            //        screenRec scrRec = new screenRec();
            //        if (scrRec.writeData(typeList, Key, logID))
            //        {
            //            setBitToTrue(recordType, logID);
            //        }
            //        break;
            //    case "12":
            //        stateRec staRec = new stateRec();
            //        if (staRec.writeData(typeList, Key, logID))
            //        {
            //            setBitToTrue(recordType, logID);
            //        }
            //        break;
            //    case "13":
            //        configParamsRec cpRec = new configParamsRec();
            //        if (cpRec.writeData(typeList, Key, logID))
            //        {
            //            setBitToTrue(recordType, logID);
            //        }
            //        break;
            //    case "15":
            //        FitRec fitRec = new FitRec();
            //        if (fitRec.writeData(typeList, Key, logID))
            //        {
            //            setBitToTrue(recordType, logID);
            //        }
            //        break;
            //    case "16":
            //        ConfigIdRec cir = new ConfigIdRec();
            //        if (cir.writeData(typeList, Key, logID))
            //        {
            //            setBitToTrue(recordType, logID);
            //        }
            //        break;
            //    case "1A":
            //        EnhancedParamsRec epRec = new EnhancedParamsRec();
            //        if (epRec.writeData(typeList, Key, logID))
            //        {
            //            setBitToTrue(recordType, logID);
            //        }
            //        break;
            //    case "1B":
            //        //writeMAC(typeList);
            //        break;
            //    case "1C":
            //        DateAndTimeRec dt = new DateAndTimeRec();
            //        if (dt.writeData(typeList, Key, logID))
            //        {
            //            setBitToTrue(recordType, logID);
            //        }
            //        break;
            //    case "1E":
            //        //writeDispenser(typeList);
            //        break;
            //    case "42":
            //        ExtEncryptionRec xer = new ExtEncryptionRec();
            //        if (xer.writeData(typeList, Key, logID))
            //        {
            //            setBitToTrue(recordType, logID);
            //        }
            //        break;

            //    case "81":
            //        // ICCCurrencyDOT iccCurrency = new ICCCurrencyDOT(new emvConfiguration());
            //        ICCCurrencyDOT iccCurrency = new ICCCurrencyDOT();
            //        if (iccCurrency.writeData(typeList, Key, logID))
            //        {
            //            setBitToTrue(recordType, logID);
            //        }
            //        break;

            //    case "82":
            //        ICCTransactionDOT iccTransaction = new ICCTransactionDOT();
            //        if (iccTransaction.writeData(typeList, Key, logID))
            //        {
            //            setBitToTrue(recordType, logID);
            //        }
            //        break;

            //    case "83":
            //        ICCLanguageSupportT iccLanguage = new ICCLanguageSupportT();
            //        if (iccLanguage.writeData(typeList, Key, logID))
            //        {
            //            setBitToTrue(recordType, logID);
            //        }
            //        break;

            //    case "84":
            //        ICCTerminalDOT iccTerminal = new ICCTerminalDOT();
            //        if (iccTerminal.writeData(typeList, Key, logID))
            //        {
            //            setBitToTrue(recordType, logID);
            //        }
            //        break;

            //    case "85":
            //        ICCApplicationIDT iccApplication = new ICCApplicationIDT();
            //        if (iccApplication.writeData(typeList, Key, logID))
            //        {
            //            setBitToTrue(recordType, logID);
            //        }
            //        break;

            //}
        }
        //todo: move to utils
        private void setBitToTrue(string recordType, string logID)
        {
            string recordTypeStr = recTypesDic[recordType];
            string sql = @"UPDATE logs SET " + recordTypeStr + " = 1 WHERE id = " + logID;

            DbCrud db = new DbCrud();
            if (db.addToDb(sql) == false) { };

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
            return dt;
        }

        public DataTable getAllLogs(string prjID)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT * FROM [dbo].[logs] WHERE prjKey = '" + prjID + "'";
            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            return dt;
        }

        public Dictionary<string, int> showRecordBits(string logID)
        {
            ///Return a Dictionary with type of record and amount

            Dictionary<string, int> dicBits = new Dictionary<string, int>();

            string sql = @"SELECT COUNT(*) FROM screeninfo WHERE logID =" + logID;

            DbCrud db = new DbCrud();

            int count = db.GetScalarFromDb(sql);
            dicBits.Add("screens", count);

                    sql = @"SELECT COUNT(*) FROM stateinfo WHERE logID =" + logID;
                    count = db.GetScalarFromDb(sql);
            dicBits.Add("states", count);

                    sql = @"SELECT COUNT(*) FROM configParamsInfo WHERE logID =" + logID;
                    count = db.GetScalarFromDb(sql);
            dicBits.Add("configParametersLoad", count);

                    sql = @"SELECT COUNT(*) FROM fitinfo WHERE logID =" + logID;
                    count = db.GetScalarFromDb(sql);
            dicBits.Add("fit", count);

                    sql = @"SELECT COUNT(*) FROM configId WHERE logID =" + logID;
                    count = db.GetScalarFromDb(sql);
            dicBits.Add("configID", count);

                    sql = @"SELECT COUNT(*) FROM enhancedParamsInfo WHERE logID =" + logID;
                    count = db.GetScalarFromDb(sql);
            dicBits.Add("enhancedParametersLoad", count);

                    sql = @"SELECT COUNT(*) FROM dateTime WHERE logID =" + logID;
                    count = db.GetScalarFromDb(sql);
            dicBits.Add("dateandtime", count);

                    sql = @"SELECT COUNT(*) FROM treq WHERE logID =" + logID;
                    count = db.GetScalarFromDb(sql);
            dicBits.Add("treq", count);

                    sql = @"SELECT COUNT(*) FROM treply WHERE logID =" + logID;
                    count = db.GetScalarFromDb(sql);
            dicBits.Add("treply", count);

                    sql = @"SELECT COUNT(*) FROM iccCurrencyDOT WHERE logID =" + logID;
                    count = db.GetScalarFromDb(sql);
            dicBits.Add("iccCurrencyDOT", count);

                    sql = @"SELECT COUNT(*) FROM iccTransactionDOT WHERE logID =" + logID;
                    count = db.GetScalarFromDb(sql);
            dicBits.Add("iccTransactionDOT", count);

                    sql = @"SELECT COUNT(*) FROM iccLanguageSupportT WHERE logID =" + logID;
                    count = db.GetScalarFromDb(sql);
            dicBits.Add("iccLanguageSupportT", count);

                    sql = @"SELECT COUNT(*) FROM iccTerminalDOT WHERE logID =" + logID;
                    count = db.GetScalarFromDb(sql);
            dicBits.Add("iccTerminalDOT", count);

                    sql = @"SELECT COUNT(*) FROM iccApplicationIDT WHERE logID =" + logID;
                    count = db.GetScalarFromDb(sql);
            dicBits.Add("iccApplicationIDT", count);

                return dicBits;

        }
       
        public new Dictionary<string, string> readData(string sql)
        {
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            Dictionary<string, string> dicData = new Dictionary<string, string>();

    //        if (dt.Rows.Count > 0)
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dicData.Add(row[0].ToString() + Convert.ToInt32(row[1]).ToString(), row[2].ToString());
                }
            }
            return dicData;
        }
    }
}
