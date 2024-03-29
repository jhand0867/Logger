﻿using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct ackEjUploadBlock
    {
        private string rectype;
        private string messageClass;
        private string commandType;
        private string lastCharReceived;

        public string Rectype { get => rectype; set => rectype = value; }
        public string LastCharReceived { get => lastCharReceived; set => lastCharReceived = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string CommandType { get => commandType; set => commandType = value; }
    };

    class AckEjUploadBlock : App, IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'J' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * FROM ackEjUploadBlock WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
                                               "' AND logkey LIKE '" + logKey + "%' LIMIT 1";
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
            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.LblTitle = "EJ Acknowledge Upload Block";
            lpb.Maximum = typeRecs.Count + 1;

            foreach (typeRec r in typeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                ackEjUploadBlock kud = parseData(r.typeContent);

                string sql = @"INSERT INTO ackEjUploadBlock([logkey],[rectype],[messageClass],[commandType],[lastCharReceived],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + kud.Rectype + "','" + kud.MessageClass + "','" + kud.CommandType + "','" +
                            kud.LastCharReceived + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;
        }

        public ackEjUploadBlock parseData(string r)
        {
            ackEjUploadBlock kud = new ackEjUploadBlock();

            string[] tmpTypes = r.Split((char)0x1c);

            kud.Rectype = "J";
            kud.MessageClass = tmpTypes[0].Substring(10, 1);
            kud.CommandType = tmpTypes[3].Substring(0, 1);
            kud.LastCharReceived = tmpTypes[3].Substring(1, 6);

            return kud;
        }
    }
}
