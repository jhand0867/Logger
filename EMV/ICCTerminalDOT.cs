using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    class ICCTerminalDOT : EMVConfiguration, IMessage
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public new DataTable getDescription()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = '8' and subRecType like '4%'";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            return dt;
        }

        public new List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * from ICCTerminalDOT WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);
            return dts;
        }

        public new bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.LblTitle = "ICC Terminal DOT";
            lpb.Maximum = typeRecs.Count + 1;

            foreach (typeRec r in typeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                string[] tmpTypes = r.typeContent.Split((char)0x1c);

                List<iccTerminal> iccTerminalList = parseData(tmpTypes[3]);
                // write Language Support Transaction
                int entries = 0;
                foreach (iccTerminal c in iccTerminalList)
                {
                    entries += 1;
                    string sql = @"INSERT INTO ICCTerminalDOT([logkey],[rectype],[responseFormat2Tag],[responseFormat2Length],
                                     [responseFormat2Value],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + c.Rectype + "','" + c.ResponseFormat2Tag + "','" + c.ResponseFormat2Length + "','" +
                                c.ResponseFormat2Value + "'," + logID + ")";

                    DbCrud db = new DbCrud();
                    if (db.crudToDb(sql) == false)
                        return false;
                }
                List<typeRec> emvList = new List<typeRec>();
                typeRec rec = r;
                rec.typeAddData = entries.ToString();
                emvList.Add(rec);
                if (base.writeData(emvList, Key, logID) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;
        }
        public new List<iccTerminal> parseData(string tmpTypes)
        {
            iccTerminal iccTerminal = new iccTerminal();
            List<iccTerminal> iccTerminalList = new List<iccTerminal>();

            iccTerminal.Rectype = "84";
            int offset = 0;

            iccTerminal.ResponseFormat2Tag = tmpTypes.Substring(offset, 2);
            offset += 2;
            iccTerminal.ResponseFormat2Length = tmpTypes.Substring(offset, 2);
            int length = Int32.Parse(tmpTypes.Substring(offset, 2), System.Globalization.NumberStyles.HexNumber) * 2;
            offset += 2;
            iccTerminal.ResponseFormat2Value = new Digester().iccTLVTags(tmpTypes.Substring(offset, length));
            iccTerminalList.Add(iccTerminal);
            return iccTerminalList;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = new List<DataTable>();
            dts = getRecord(logKey, logID, projectKey);
            string txtField = "";
            string[] descriptionFields = new string[] { "", "", "" };

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            txtField = base.parseToView(logKey, logID, projectKey, recValue);

            DataTable iccRecDt = this.getDescription();

            if (dts[0].Rows.Count > 0)
                for (int rowNum = 0; rowNum < dts[0].Rows.Count; rowNum++)
                {
                    // Configuration Data

                    descriptionFields[0] = "Configuration Data Parsing: ";
                    descriptionFields[1] = "";
                    descriptionFields[2] = "";

                    txtField += App.Prj.insertRowRtf(descriptionFields);

                    for (int colNum = 3; colNum < dts[0].Columns.Count - 1; colNum++)
                        txtField += App.Prj.getOptionDescription(iccRecDt, "4" + colNum.ToString("00"), dts[0].Rows[rowNum][colNum].ToString());
                }

            return txtField;
        }
    }

}
