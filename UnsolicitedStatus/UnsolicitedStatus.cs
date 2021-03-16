using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    class UnsolicitedStatus : IMessage
    {

        public Dictionary<string, string> usTypes = new Dictionary<string, string>();

        public UnsolicitedStatus()
        {
            usTypes.Add("A", "12A");
            usTypes.Add("B", "12B");
            usTypes.Add("D", "12D");
            usTypes.Add("E", "12E");
            usTypes.Add("F", "12F");
            usTypes.Add("G", "12G");
            usTypes.Add("H", "12H");
            usTypes.Add("K", "12K");
            usTypes.Add("L", "12L");
            usTypes.Add("M", "12M");
            usTypes.Add("P", "12P");
            usTypes.Add("Q", "12Q");
            usTypes.Add("R", "12R");
            usTypes.Add("S", "12S");
            usTypes.Add("V", "12V");
            usTypes.Add("a", "1261");
            usTypes.Add("w", "12W");
            usTypes.Add("q", "1271");
            usTypes.Add("Y", "12Y");
            usTypes.Add("f", "1266");
            usTypes.Add("c", "12C");
            usTypes.Add(@"\", "125C");
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            throw new NotImplementedException();
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            throw new NotImplementedException();
        }

        public DataTable getDescription(string recType)
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'U'  AND subRecType like '" + recType + "%'";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey, string recType)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT TOP 1 * FROM unsolicitedStatus" + recType + "  WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
                                               "' AND logkey LIKE '" + logKey + "%'";

            DataTable dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {

            string recordType = getRecordType(recValue);
            List<DataTable> dts = getRecord(logKey, logID, projectKey, recordType.Substring(2, recordType.Length - 2));
            string txtField = "";

            if (dts == null || dts[0] == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable us = getDescription(recordType.Substring(2, recordType.Length - 2));

            if (dts[0].Rows.Count > 0)
            {
                for (int colNum = 3; colNum < dts[0].Columns.Count - 2; colNum++)
                {
                    if (dts[0].Rows[0][colNum].ToString() != " ")
                    {
                        txtField += getOptionDescription(us, recordType.Substring(2, recordType.Length - 2) + colNum.ToString("00"),
                                         dts[0].Rows[0][colNum].ToString());
                        txtField += "\t" + System.Environment.NewLine;
                    }

                }
            }
            return txtField;
        }

        public virtual bool writeData(List<typeRec> typeRecs, string key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                List<typeRec> OneTypeRec = new List<typeRec>();
                OneTypeRec.Add(r);

                string recordType = getRecordType(r.typeContent);

                IMessage theRecord = LoggerFactory.Create_Record(recordType);

                // MLH Temporary check until all the unsolicited messages are created

                if (theRecord == null) continue;

                if (theRecord.writeData(OneTypeRec, key, logID) == false)
                    return false;
            }
            return true;
        }

        internal string getOptionDescription(DataTable dataTable, string field, string fieldValue)
        {
            string optionDesc = "";
            string fieldDesc = "";

            // what's the description of the field
            foreach (DataRow item in dataTable.Rows)
            {
                if (item[2].ToString().Trim() == field)
                {
                    optionDesc = item[3].ToString().Trim();

                    if (item[5].ToString() != null && item[5].ToString() != "")
                    {
                        Digester myDigester = LoggerFactory.Create_Digester();
                        fieldDesc = myDigester.fieldDigester(item[5].ToString(), fieldValue);
                        fieldValue = fieldValue.Replace(";", " ");
                    }
                    optionDesc += " = " + fieldValue + insertDescription(item[4].ToString()) + fieldDesc;

                    break;
                }
            }
            return optionDesc;
        }

        internal string getRecordType(string recValue)
        {
            string[] tmpTypes = recValue.Split((char)0x1c);
            return usTypes[tmpTypes[3].Substring(0, 1)];
        }

        private string insertDescription(string fieldDescription)
        {
            string description = "";

            if (fieldDescription != "")
            {                
                if (fieldDescription.Contains("\r\n"))
                {
                    description += System.Environment.NewLine + fieldDescription.Trim() + System.Environment.NewLine;
                }
                else
                {
                    //description += "\t" + fieldDescription.Trim() + System.Environment.NewLine;
                    description += "\t" + fieldDescription.Trim();
                }
            }
            else
            {
                description += fieldDescription.Trim();
            }
            return description;
        }
    }
}
