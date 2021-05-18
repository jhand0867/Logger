﻿using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct ackStopEj
    {
        private string rectype;
        private string lastCharReceived;

        public string Rectype { get => rectype; set => rectype = value; }
        public string LastCharReceived { get => lastCharReceived; set => lastCharReceived = value; }
    };

    class AckStopEj : IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'L' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT TOP 1 * FROM ackStopEj WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
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
                for (int colNum = 3; colNum < dts[0].Columns.Count - 2; colNum++)
                    txtField += App.Prj.getOptionDescription(ss, colNum.ToString("00"), dts[0].Rows[0][colNum].ToString());

            return txtField;
        }

        public bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                ackStopEj ase = parseData(r.typeContent);

                string sql = @"INSERT INTO ackStopEj([logkey],[rectype],[lastCharReceived],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + ase.Rectype + "','" + ase.LastCharReceived + "','" +
                               Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public ackStopEj parseData(string r)
        {
            ackStopEj ase = new ackStopEj();

            string[] tmpTypes = r.Split((char)0x1c);

            ase.Rectype = "L";
            ase.LastCharReceived = tmpTypes[3].Substring(1, 1);

            return ase;
        }


    }
}
