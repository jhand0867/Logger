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
    }
    public class Project : App
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string pKey;
        private string pName;
        private string pBrief;
        private int pLogs;

        // Initialize all record types
        private string[] recordTypes = { "ATM2HOST: 11", "HOST2ATM: 4", "HOST2ATM: 3" };
        private string[] subRecordTypes = { "11", "12", "13", "15", "16", "1A", "1B", "1C", "1E" };
        private List<stateRec> extensionsLst = new List<stateRec>();
        public List<stateRec> ExtensionsLst
        {
            get { return extensionsLst; }
            set { extensionsLst = value; }
        }
        public string[] RecordTypes
        {
            get { return recordTypes; }
        }
        public string[] SubRecordTypes
        {
            get { return subRecordTypes; }
        }

        public Project()
        {
            pKey = "";
            pName = "";
            pBrief = "";
            pLogs = 0;
        }
        public Project(string pName, string pBrief)
        {
            createProject(pName, pBrief);
        }
        public Project getProjectByID(string prjID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            string sql;
            SqlConnection cnn;

            cnn = new SqlConnection(connectionString);

            sql = @"SELECT * FROM project WHERE prjKey='" + prjID + "'";

            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataReader dataReader;

                command = new SqlCommand(sql, cnn);

                dataReader = command.ExecuteReader();

                Project pr = new Project();

                while (dataReader.Read())
                {
                    pr.pKey = dataReader.GetString(1);
                    pr.Name = dataReader.GetString(2);
                    pr.Brief = dataReader.GetString(3);
                    pr.pLogs = Convert.ToInt32(dataReader.GetBoolean(4));
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
                return pr;
            }
            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return null;
            }
        }

        public Dictionary<string, Project> getAllProjects()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            string sql;
            SqlConnection cnn;

            cnn = new SqlConnection(connectionString);

            sql = @"SELECT * FROM project";

            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataReader dataReader;

                command = new SqlCommand(sql, cnn);

                dataReader = command.ExecuteReader();

                Dictionary<string, Project> dicData = new Dictionary<string, Project>();

                while (dataReader.Read())
                {
                    Project pr = new Project();
                    pr.pKey = dataReader.GetString(1);
                    pr.Name = dataReader.GetString(2);
                    pr.Brief = dataReader.GetString(3);
                    pr.pLogs = dataReader.GetInt32(4);
                    dicData.Add(dataReader.GetString(1) + dataReader.GetInt32(0).ToString(), pr);
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
                return dicData;
            }
            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return null;
            }
        }

        public Dictionary<string, Project> getProjectByName(string pName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            string sql;
            SqlConnection cnn;

            cnn = new SqlConnection(connectionString);

            sql = @"SELECT * FROM project WHERE prjName ='" + pName + "'";

            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataReader dataReader;

                command = new SqlCommand(sql, cnn);

                dataReader = command.ExecuteReader();

                Dictionary<string, Project> dicData = new Dictionary<string, Project>();

                while (dataReader.Read())
                {
                    Project pr = new Project();
                    pr.pKey = dataReader.GetString(1);
                    pr.Name = dataReader.GetString(2);
                    pr.Brief = dataReader.GetString(3);
                    pr.pLogs = dataReader.GetInt32(4);
                    dicData.Add(dataReader.GetString(1) + dataReader.GetInt32(0).ToString(), pr);
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
                return dicData;
            }
            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return null;
            }
        }
        public bool updateProjectByName(Project project, string pName, string pBrief)
        {
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                String sql = "";

                sql = @"UPDATE Project SET prjName ='" + pName +
                       "', prjBrief ='" + pBrief +
                       "' WHERE prjKey ='" + project.pKey + "'";

                command = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand.ExecuteNonQuery();

                command.Dispose();
                cnn.Close();
                return true;

            }
            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return false;
            }
        }
        public bool addLogToProject(string pKey)
        {
            string connectionString;
            string sql;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;

            cnn = new SqlConnection(connectionString);

            sql = @"UPDATE Project SET prjLogs = prjLogs + 1 " +
                   "WHERE prjKey ='" + pKey + "'";

            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataReader dataReader;

                command = new SqlCommand(sql, cnn);

                dataReader = command.ExecuteReader();

                dataReader.Close();
                command.Dispose();
                cnn.Close();
                return true;
            }
            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return false;
            }

        }

        public int attachLogToProject(string pKey, string pFilename)
        {
            string connectionString;
            string sql;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;

            cnn = new SqlConnection(connectionString);

            sql = @"INSERT INTO logs(prjKey, logFile, uploadDate) 
                    VALUES('" + pKey + "','" + pFilename + "', GETDATE()); SELECT CAST(scope_identity() AS int);";

            try
            {
                cnn.Open();

                SqlCommand command;
                int newLogID;

                command = new SqlCommand(sql, cnn);

                newLogID = (Int32)command.ExecuteScalar();

                command.Dispose();
                cnn.Close();
                return newLogID;
            }
            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return 0;
            }

        }

        public Dictionary<string, Project> createProject(string name, string brief)
        {
            Name = name;
            Brief = brief;
            pLogs = 0;
            pKey = Guid.NewGuid().ToString();

            /**********/

            string connectionString;
            string sql;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);

            sql = @"INSERT INTO Project(prjKey,prjName,prjBrief,prjLogs,createDate)" +
                   "VALUES('" + Key + "','" + Name + "','" + Brief + "','" + 0 + "',GETDATE()" + ")";

            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataReader dataReader;

                command = new SqlCommand(sql, cnn);

                dataReader = command.ExecuteReader();

                Dictionary<string, Project> dicData = new Dictionary<string, Project>();

                while (dataReader.Read())
                {
                    Project pr = new Project();
                    pr.pKey = dataReader.GetString(1);
                    pr.Name = dataReader.GetString(2);
                    pr.Brief = dataReader.GetString(3);
                    pr.pLogs = dataReader.GetInt32(4);
                    dicData.Add(dataReader.GetString(1) + dataReader.GetInt32(0).ToString(), pr);
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
                return dicData;
            }
            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return null;
            }
        }

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
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
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

                command = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand.ExecuteNonQuery();

                command.Dispose();
                cnn.Close();

            }
            catch (SqlException dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
            }

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

                if (recordType == "00" || recordType == "01")
                {
                    typeRec r = new typeRec();
                    r.typeIndex = rec.Key;
                    r.typeContent = rec.Value;
                    typeList.Add(r);
                    continue;
                }

                tmpTypes = rec.Value.Split((char)0x1c);


                if (recordType.Substring(0, 1) == "8" &&
                   ((recordType.Substring(1, 1) == "1" && tmpTypes[2] == "1") ||
                    (recordType.Substring(1, 1) == "2" && tmpTypes[2] == "2")))
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
            switch (recordType)
            {
                case "00":
                    TRec tr = new TRec();
                    if (tr.writeData(typeList, Key, logID))
                    {
                        setBitToTrue(recordType, logID);
                    }

                    break;
                case "01":
                    TReply treply = new TReply();
                    if (treply.writeData(typeList, Key, logID))
                    {
                        // set screen bit to true
                        setBitToTrue(recordType, logID);
                    }
                    break;
                case "11":
                    screenRec scrRec = new screenRec();
                    if (scrRec.writeData(typeList, Key, logID))
                    {
                        setBitToTrue(recordType, logID);
                    }
                    break;
                case "12":
                    stateRec staRec = new stateRec();
                    if (staRec.writeData(typeList, Key, logID))
                    {
                        setBitToTrue(recordType, logID);
                    }
                    break;
                case "13":
                    configParamsRec cpRec = new configParamsRec();
                    if (cpRec.writeData(typeList, Key, logID))
                    {
                        setBitToTrue(recordType, logID);
                    }
                    break;
                case "15":
                    FitRec fitRec = new FitRec();
                    if (fitRec.writeData(typeList, Key, logID))
                    {
                        setBitToTrue(recordType, logID);
                    }
                    break;
                case "16":
                    ConfigIdRec cir = new ConfigIdRec();
                    if (cir.writeData(typeList, Key, logID))
                    {
                        setBitToTrue(recordType, logID);
                    }
                    break;
                case "1A":
                    EnhancedParamsRec epRec = new EnhancedParamsRec();
                    if (epRec.writeData(typeList, Key, logID))
                    {
                        setBitToTrue(recordType, logID);
                    }
                    break;
                case "1B":
                    //writeMAC(typeList);
                    break;
                case "1C":
                    DateAndTimeRec dt = new DateAndTimeRec();
                    if (dt.writeData(typeList, Key, logID))
                    {
                        setBitToTrue(recordType, logID);
                    }
                    break;
                case "1E":
                    //writeDispenser(typeList);
                    break;
                case "42":
                    ExtEncryptionRec xer = new ExtEncryptionRec();
                    if (xer.writeData(typeList, Key, logID))
                    {
                        setBitToTrue(recordType, logID);
                    }
                    break;

                case "81":
                    // ICCCurrencyDOT iccCurrency = new ICCCurrencyDOT(new emvConfiguration());
                    ICCCurrencyDOT iccCurrency = new ICCCurrencyDOT();
                    if (iccCurrency.writeData(typeList, Key, logID))
                    {
                        setBitToTrue(recordType, logID);
                    }
                    break;

                case "82":
                    ICCTransactionDOT iccTransaction = new ICCTransactionDOT();
                    if (iccTransaction.writeData(typeList, Key, logID))
                    {
                        setBitToTrue(recordType, logID);
                    }
                    break;

            }
        }

        private void setBitToTrue(string recordType, string logID)
        {
            string sql = "";
            switch (recordType)
            {
                case "00":
                    sql = @"UPDATE logs SET treq = 1 WHERE id = " + logID;
                    break;
                case "01":
                    sql = @"UPDATE logs SET treply = 1 WHERE id = " + logID;
                    break;
                case "11":
                    sql = @"UPDATE logs SET screens = 1 WHERE id = " + logID;
                    break;
                case "12":
                    sql = @"UPDATE logs SET states = 1 WHERE id = " + logID;
                    break;
                case "13":
                    sql = @"UPDATE logs SET configParametersLoad = 1 WHERE id = " + logID;
                    break;
                case "15":
                    sql = @"UPDATE logs SET fit = 1 WHERE id = " + logID;
                    break;
                case "16":
                    sql = @"UPDATE logs SET configID = 1 WHERE id = " + logID;
                    break;
                case "1A":
                    sql = @"UPDATE logs SET enhancedParametersLoad = 1 WHERE id = " + logID;
                    break;
                case "1B":
                    sql = @"UPDATE logs SET mac = 1 WHERE id = " + logID;
                    break;
                case "1C":
                    sql = @"UPDATE logs SET dateandtime = 1 WHERE id = " + logID;
                    break;
                case "81":
                    sql = @"UPDATE logs SET iccCurrencyDOT = 1 WHERE id = " + logID;
                    break;
                case "82":
                    sql = @"UPDATE logs SET iccTransactionDOT = 1 WHERE id = " + logID;
                    break;

            }

            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                command = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand.ExecuteNonQuery();

                command.Dispose();
                cnn.Close();
            }
            catch (SqlException dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
            }
        }
        public DataTable getGroupOptions(string logID, string fieldName)
        {
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            try
            {
                cnn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT DISTINCT " + fieldName + " FROM loginfo WHERE logID =" +
                                                               logID + " ORDER BY " + fieldName + " ASC", cnn))
                {
                    sda.Fill(dt);

                    return dt;
                }
            }
            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return null;
            }
        }

        public DataTable getALogByIDWithCriteria(string logID, string columnName, string columnValue)
        {
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            string sql = "";

            switch (columnName)
            {
                case "group6":
                    sql = @"SELECT [id],[logkey],[group1] as 'Timestamp',
                            [group2] as 'Log Level',[group3] as 'File Name',
                            [group4] as 'Class',[group5] as 'Method',
                            [group6] as 'Type',[group7],
                            [group8] as 'Log Data',[group9],
                            [prjKey],[logID] FROM [loginfo] WHERE logID =" + logID +
                        " AND " + columnName + " LIKE '%[[]" + columnValue + "%'";
                    break;
                case "group8":
                    sql = @"SELECT [id],[logkey],[group1] as 'Timestamp',
                            [group2] as 'Log Level',[group3] as 'File Name',
                            [group4] as 'Class',[group5] as 'Method',
                            [group6] as 'Type',[group7],
                            [group8] as 'Log Data',[group9],
                            [prjKey],[logID] FROM [loginfo] WHERE logID=" + logID +
                        " AND " + columnName + " LIKE '%" + columnValue + "%'";
                    break;
                default:
                    sql = @"SELECT [id],[logkey],[group1] as 'Timestamp',
                            [group2] as 'Log Level',[group3] as 'File Name',
                            [group4] as 'Class',[group5] as 'Method',
                            [group6] as 'Type',[group7],
                            [group8] as 'Log Data',[group9],
                            [prjKey],[logID] FROM [loginfo] WHERE logID =" + logID +
                        " AND " + columnName + "='" + columnValue + "'";
                    break;
            }



            try
            {
                cnn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(sql, cnn))
                {
                    sda.Fill(dt);

                    return dt;
                }
            }
            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return null;
            }
        }
        public DataTable getALogByID(string logID)
        {
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            try
            {
                cnn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT [id],[logkey],[group1] as 'Timestamp',
                                                              [group2] as 'Log Level',[group3] as 'File Name',
                                                              [group4] as 'Class',[group5] as 'Method',
                                                              [group6] as 'Type',[group7],
                                                              [group8] as 'Log Data',[group9],
                                                              [prjKey],[logID] FROM [loginfo] 
                                                              WHERE logID =" + logID, cnn))
                {
                    sda.Fill(dt);

                    return dt;
                }
            }
            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return null;
            }
        }
        public DataTable getAllLogs(string prjID)
        {
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            try
            {
                cnn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [dbo].[logs] WHERE prjKey = '" + prjID + "'", cnn))
                {
                    sda.Fill(dt);

                    return dt;
                }
            }
            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return null;
            }
        }

        public Dictionary<string, int> showRecordBits(string logID)
        {
            ///Return a Dictionary with type of record and amount

            string connectionString;
            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;


            Dictionary<string, int> dicBits = new Dictionary<string, int>();

            SqlConnection cnn = new SqlConnection(connectionString);

            try
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT COUNT(*) FROM screeninfo WHERE logID =" + logID, cnn))
                {
                    cnn.Open();

                    int count = (int)cmd.ExecuteScalar();
                    dicBits.Add("screens", count);

                    cmd.CommandText = @"SELECT COUNT(*) FROM stateinfo WHERE logID =" + logID;
                    count = (int)cmd.ExecuteScalar();
                    dicBits.Add("states", count);

                    cmd.CommandText = @"SELECT COUNT(*) FROM configParamsInfo WHERE logID =" + logID;
                    count = (int)cmd.ExecuteScalar();
                    dicBits.Add("configParams", count);

                    cmd.CommandText = @"SELECT COUNT(*) FROM fitinfo WHERE logID =" + logID;
                    count = (int)cmd.ExecuteScalar();
                    dicBits.Add("fit", count);

                    cmd.CommandText = @"SELECT COUNT(*) FROM configId WHERE logID =" + logID;
                    count = (int)cmd.ExecuteScalar();
                    dicBits.Add("configId", count);

                    cmd.CommandText = @"SELECT COUNT(*) FROM enhancedParamsInfo WHERE logID =" + logID;
                    count = (int)cmd.ExecuteScalar();
                    dicBits.Add("enhancedParams", count);

                    cmd.CommandText = @"SELECT COUNT(*) FROM dateTime WHERE logID =" + logID;
                    count = (int)cmd.ExecuteScalar();
                    dicBits.Add("dateTime", count);

                    cmd.CommandText = @"SELECT COUNT(*) FROM treq WHERE logID =" + logID;
                    count = (int)cmd.ExecuteScalar();
                    dicBits.Add("treq", count);

                    cmd.CommandText = @"SELECT COUNT(*) FROM treply WHERE logID =" + logID;
                    count = (int)cmd.ExecuteScalar();
                    dicBits.Add("treply", count);

                    cmd.CommandText = @"SELECT COUNT(*) FROM iccCurrencyDOT WHERE logID =" + logID;
                    count = (int)cmd.ExecuteScalar();
                    dicBits.Add("iccCurrencyDOT", count);

                    cmd.CommandText = @"SELECT COUNT(*) FROM iccTransactionDOT WHERE logID =" + logID;
                    count = (int)cmd.ExecuteScalar();
                    dicBits.Add("iccTransactionDOT", count);

                }

                return dicBits;
            }
            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return null;
            }

        }
        public new Dictionary<string, string> readData(string sql)
        {

            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataReader dataReader;

                command = new SqlCommand(sql, cnn);
                // dataReader.GetOrdinal(0);

                dataReader = command.ExecuteReader();

                Dictionary<string, string> dicData = new Dictionary<string, string>();




                while (dataReader.Read())
                {
                    dicData.Add(dataReader.GetString(0) + dataReader.GetInt32(1).ToString(), dataReader.GetString(2));
                }

                dataReader.Close();
                command.Dispose();
                cnn.Close();
                return dicData;
            }
            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return null;
            }
        }
    }
}
