using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    class ICCCurrencyDOT : EMVConfiguration, IMessage
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public new DataTable getDescription()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = '8' and subRecType like '1%'";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            return dt;
        }

        public new List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();

            // dt = new DataTable();
            string sql = @"SELECT * from ICCCurrencyDOT WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);
            return dts;

        }

        public new bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.LblTitle = "ICC Currency DOT";
            lpb.Maximum = typeRecs.Count + 1;

            foreach (typeRec r in typeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                string[] tmpTypes = r.typeContent.Split((char)0x1c);

                List<iccCurrency> iccCurrencyDOTList = parseData(tmpTypes[3]);

                // write currency DOT
                foreach (iccCurrency c in iccCurrencyDOTList)
                {
                    string sql = @"INSERT INTO ICCCurrencyDOT([logkey],[rectype],[currencyType],[responseFormat2Tag],
                                [responseFormat2Length],[responseFormat2Value],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + c.Rectype + "','" + c.CurrencyType + "','" + c.ResponseFormat2Tag + "','" +
                                c.ResponseFormat2Length + "','" + c.ResponseFormat2Value + "'," + logID + ")";

                    DbCrud db = new DbCrud();
                    if (db.crudToDb(sql) == false)
                        return false;
                }

                //EMVConfiguration emv = new EMVConfiguration();
                List<typeRec> emvList = new List<typeRec>();
                emvList.Add(r);
                if (base.writeData(emvList, Key, logID) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;
        }


        public new List<iccCurrency> parseData(string r)
        {
            List<iccCurrency> iccCurrencyDOTList = new List<iccCurrency>();
            iccCurrency iccCurrency = new iccCurrency();
            log.Debug("Parsing ICCCurrencyDOT data");
            int offset = 2;

            for (int x = 0; x < int.Parse(r.Substring(0, 2)); x++)
            {
                iccCurrency.Rectype = "81";
                iccCurrency.CurrencyType = r.Substring(offset, 2);
                offset += 2;
                iccCurrency.ResponseFormat2Tag = r.Substring(offset, 2);
                offset += 2;
                iccCurrency.ResponseFormat2Length = r.Substring(offset, 2);
                int length = Int32.Parse(r.Substring(offset, 2), System.Globalization.NumberStyles.HexNumber) * 2;
                offset += 2;
                iccCurrency.ResponseFormat2Value = new Digester().iccTLVTags(r.Substring(offset, length));
                iccCurrencyDOTList.Add(iccCurrency);
                offset += length;
            }
            log.Debug("Returning Parsed data:" + iccCurrencyDOTList.ToString());
            return iccCurrencyDOTList;
        }

        public override string parseToView(string logKey, string logID, string projectKey, string recValue)
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
                        txtField += App.Prj.getOptionDescription(iccRecDt, "1" + colNum.ToString("00"), dts[0].Rows[rowNum][colNum].ToString());
                }
            return txtField;
        }
    }
}
