﻿using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct macFieldSelection
    {
        private string rectype;
        private string treqField;
        private string treplyField;
        private string ssField;
        private string otherMsgsField;
        private string track1Field;
        private string track2Field;
        private string track3Field;
        private string emvConfig;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string TreqField { get => treqField; set => treqField = value; }
        public string TreplyField { get => treplyField; set => treplyField = value; }
        public string SsField { get => ssField; set => ssField = value; }
        public string OtherMsgsField { get => otherMsgsField; set => otherMsgsField = value; }
        public string Track1Field { get => track1Field; set => track1Field = value; }
        public string Track2Field { get => track2Field; set => track2Field = value; }
        public string Track3Field { get => track3Field; set => track3Field = value; }
        public string EmvConfig { get => emvConfig; set => emvConfig = value; }
        public string Mac { get => mac; set => mac = value; }
    };

    class MACFieldSelection : IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'B' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT TOP 1 * FROM macFieldSelection WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
                                               "' AND logkey LIKE '" + logKey + "%'";
            DataTable dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable ss = getDescription();

            if (dts[0].Rows.Count > 0)
            {
                for (int colNum = 3; colNum < dts[0].Columns.Count - 2; colNum++)
                {
                    txtField += getOptionDescription(ss, colNum.ToString("00"), dts[0].Rows[0][colNum].ToString());
                    txtField += "\t" + System.Environment.NewLine;

                }
            }
            return txtField;
        }

        public bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                macFieldSelection mfs = parseData(r.typeContent);

                string sql = @"INSERT INTO macFieldSelection([logkey],[rectype],[treqField],[treplyField],[ssField],
                                            [otherMsgsField],[track1Field],[track2Field],[track3Field],[emvConfig],
                                            [mac],[prjkey],[logID])" +
                            " VALUES('" + r.typeIndex + "','" + mfs.Rectype + "','" + mfs.TreqField + "','" + mfs.TreplyField + "','" +
                             mfs.SsField + "','" + mfs.OtherMsgsField + "','" + mfs.Track1Field + "','" + mfs.Track2Field + "','" +
                             mfs.Track3Field + "','" + mfs.EmvConfig + "','" + mfs.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public macFieldSelection parseData(string r)
        {
            macFieldSelection mfs = new macFieldSelection();

            string[] tmpTypes = r.Split((char)0x1c);

            mfs.Rectype = "B";

            if (tmpTypes.Length > 4)
                mfs.TreqField = tmpTypes[4];

            if (tmpTypes.Length > 5)
                mfs.TreplyField = tmpTypes[5];

            if (tmpTypes.Length > 6)
                mfs.SsField = tmpTypes[6];

            if (tmpTypes.Length > 7)
                mfs.OtherMsgsField = tmpTypes[7];

            if (tmpTypes.Length > 8)
                mfs.Track1Field = tmpTypes[8];

            if (tmpTypes.Length > 9)
                mfs.Track2Field = tmpTypes[9];

            if (tmpTypes.Length > 10)
                mfs.Track3Field = tmpTypes[10];

            int i = 11;

            if ((tmpTypes.Length > i) &&
                tmpTypes[i].Length != 8)
            {
                mfs.EmvConfig = tmpTypes[i];
                i++;
            }

            if (tmpTypes.Length > i)
                mfs.Mac = tmpTypes[i];

            return mfs;
        }

        internal string getOptionDescription(DataTable dataTable, string field, string fieldValue)
        {

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
                    description += "\t" + fieldDescription.Trim() + System.Environment.NewLine;
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

