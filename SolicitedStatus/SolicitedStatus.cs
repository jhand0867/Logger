using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Windows.Forms;

namespace Logger
{

    class SolicitedStatus : IMessage
    {

        public Dictionary<string, string> ssTypes = new Dictionary<string, string>();

    
        public SolicitedStatus()
        {
            ssTypes.Add("A", "229");
            ssTypes.Add("B", "22B");
            ssTypes.Add("8", "228");
            ssTypes.Add("9", "229");
            ssTypes.Add("C", "22C");
            ssTypes.Add("F1", "22F1");
            ssTypes.Add("F2", "22F2");
            ssTypes.Add("F3", "22F3");
            ssTypes.Add("F4", "22F4");
            ssTypes.Add("F5", "22F5");
            ssTypes.Add("F6", "22F6");
            ssTypes.Add("F7", "22F7");
            ssTypes.Add("FF", "22FF");
            ssTypes.Add("FH", "22FH");
            ssTypes.Add("FI", "22FI");
            ssTypes.Add("FJ", "22FJ");
            ssTypes.Add("FK", "22FK");
            ssTypes.Add("FL", "22FL");
            ssTypes.Add("FM", "22FM");
            ssTypes.Add("FN", "22FN");
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            throw new NotImplementedException();
        }

        public DataTable getDescription(string recType)
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'N'  AND subRecType like '" + recType + "%'";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            throw new NotImplementedException();
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey, string recType)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT TOP 1 * FROM solicitedStatus" + recType + " WHERE prjkey = '" + projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" +
                                                               logKey + "%'";
            DataTable dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            string recordType = getRecordType(recValue);
            List<DataTable> dts = getRecord(logKey, logID, projectKey, recordType.Substring(2, recordType.Length - 2));
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable ss = getDescription(recordType.Substring(2, recordType.Length - 2));

            if (dts[0].Rows.Count > 0)
            {
                for (int colNum = 3; colNum < dts[0].Columns.Count - 2; colNum++)
                {
                        txtField += getOptionDescription(ss, recordType.Substring(2, recordType.Length - 2) + colNum.ToString("00"),
                                                             dts[0].Rows[0][colNum].ToString());
                        txtField += "\t" + System.Environment.NewLine;

                }
            }
            return txtField;

        }

        public virtual bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                List<typeRec> OneTypeRec = new List<typeRec>();
                OneTypeRec.Add(r);

                string recordType = getRecordType(r.typeContent);

                    IMessage theRecord = MessageFactory.Create_Record(recordType);

                    if (theRecord.writeData(OneTypeRec, Key, logID) == false)
                        return false;
            }
            return true;
        }

        internal string getOptionDescription(DataTable dataTable, string field, string fieldValue)
        {

            // todo: Scanning all records for samples
            // todo: enter data descriptions for all records
            // todo: put together the digesting routines for all record types

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
                        Digester myDigester = MessageFactory.Create_Digester();
                        fieldDesc = myDigester.fieldDigester(item[5].ToString(), fieldValue);
                        fieldValue = fieldValue.Replace(";", " ");
                        //optionDesc += " = " + App.Prj.getEMVTags(fieldValue);
                    }

                    optionDesc += " = " + fieldValue + fieldDesc;
                    
                    break;
                }
            }
            return optionDesc;
        }

        internal string getRecordType(string recValue)
        {
            string[] tmpTypes = recValue.Split((char)0x1c);

            string recordType = "";
            int i = 3;

            string[] tmp = tmpTypes[3].Split((char)0x1d);

            if (tmp[0].Length != 1) i = 4;

            if (tmpTypes[i] == "F")
            {
                recordType = ssTypes[tmpTypes[i] + tmpTypes[i + 1].Substring(0, 1)];
            }
            else
            {
                recordType = ssTypes[tmpTypes[i].Substring(0, 1)];
            }

            return recordType;
        }

        //internal string fieldDigester(string fieldType, string fieldValue)
        //{

        //    string fieldDesc = System.Environment.NewLine;
        //    string[] tmpfieldValue = fieldValue.Split(';');
        //    DataTable dataTable = getDescriptionX(fieldType);

        //    // Use fieldType = 1 when fieldvalue from message is a "Tag Length Value (TLV)" format

        //    if (fieldType == "1")
        //    {
        //        foreach (string field in tmpfieldValue)
        //        {
        //            string[] tlv = field.Split(' ');
        //            foreach (DataRow item in dataTable.Rows)
        //            {
        //                if (item[2].ToString().Trim() == tlv[0])
        //                {
        //                    fieldDesc = fieldDesc + "   " + item[3].ToString().Trim() + " = " + tlv[0];
        //                    if (tlv.Length > 1)
        //                    {
        //                        fieldDesc = fieldDesc + " Length = " + tlv[1] + " Value = " + tlv[2];
        //                    }
        //                    fieldDesc = fieldDesc + System.Environment.NewLine;
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    // Use fieldType = 2 when fieldvalue from message is equal to subRecType in the Data Description

        //    if (fieldType == "2")
        //    {
        //        foreach (string field in tmpfieldValue)
        //        {
        //            foreach (DataRow item in dataTable.Rows)
        //            {
        //                if (item[2].ToString().Trim() == field)
        //                {
        //                    fieldDesc = fieldDesc + "   " + item[3].ToString().Trim() + " = " + item[4].ToString().Trim() + System.Environment.NewLine;
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    // Use fieldType = 3 when
        //    // first char of fieldvalue from message is equal to first char of subRecType in the Data Description
        //    // and the position of next char(s) of fieldvalue from message is equal to subRecType 
        //    // i.e. field value = E11121
        //    //      will search for E0 in data description table to show the description for E
        //    //      and then will search for each of the positions (E1,E2,E3,E4, etc) in data description table


        //    if (fieldType == "3")
        //    {
        //        bool headerFlg = false;

        //        foreach (string field in tmpfieldValue)
        //        {
        //            int pos = 0;
        //            foreach (DataRow item in dataTable.Rows)
        //            {
        //                if (!headerFlg && item[2].ToString().Trim().Length == 0)
        //                {
        //                    fieldDesc = fieldDesc + item[3].ToString().Trim() + System.Environment.NewLine;
        //                    headerFlg = true;
        //                    continue;
        //                }
        //                if (item[2].ToString().Trim().Length > 0 && 
        //                    item[2].ToString().Trim().Substring(0,1) == field.Substring(0,1) &&
        //                    field.Length > pos)
        //                {
        //                    fieldDesc = fieldDesc + "   " + item[3].ToString().Trim() + " = " + field.Substring(pos,1) + System.Environment.NewLine;
        //                    pos++;                         
        //                }
        //            }
        //        }
        //    }
        //    return fieldDesc;
        //}

        //public DataTable getDescriptionX(string fieldType)
        //{
        //    string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'X'  AND fieldType = '" + fieldType + "' order by subRecType asc";

        //    DbCrud db = new DbCrud();
        //    DataTable dt = db.GetTableFromDb(sql);
        //    return dt;
        //}

    }
}
