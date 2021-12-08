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
            lpb.LblTitle = this.ToString();
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
                    string sql = @"INSERT INTO ICCCurrencyDOT([logkey],[rectype],[currencyType],[responseFormat],[responseLength],
	                            [trCurrencyCodeTag],[trCurrencyCodeLgth],[trCurrencyCodeValue],[trCurrencyExpTag],
	                            [trCurrencyExpLgth],[trCurrencyExpValue],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + c.Rectype + "','" + c.CurrencyType + "','" + c.ResponseFormat + "','" +
                                c.ResponseLength + "','" + c.TrCurrencyCodeTag + "','" + c.TrCurrencyCodeLgth + "','" +
                                c.TrCurrencyCodeValue + "','" + c.TrCurrencyExpTag + "','" + c.TrCurrencyExpLgth + "','" +
                                c.TrCurrencyExpValue + "'," + logID + ")";

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
                iccCurrency.ResponseFormat = r.Substring(offset, 2);
                offset += 2;
                iccCurrency.ResponseLength = r.Substring(offset, 2);
                offset += 2;
                iccCurrency.TrCurrencyCodeTag = r.Substring(offset, 4);
                offset += 4;
                iccCurrency.TrCurrencyCodeLgth = r.Substring(offset, 2);
                offset += 2;
                iccCurrency.TrCurrencyCodeValue = r.Substring(offset, int.Parse(iccCurrency.TrCurrencyCodeLgth) * 2);
                offset += int.Parse(iccCurrency.TrCurrencyCodeLgth) * 2;
                iccCurrency.TrCurrencyExpTag = r.Substring(offset, 4);
                offset += 4;
                iccCurrency.TrCurrencyExpLgth = r.Substring(offset, 2);
                offset += 2;
                iccCurrency.TrCurrencyExpValue = r.Substring(offset, int.Parse(iccCurrency.TrCurrencyExpLgth) * 2);
                offset += int.Parse(iccCurrency.TrCurrencyExpLgth) * 2;

                iccCurrencyDOTList.Add(iccCurrency);

            }
            log.Debug("Returning Parsed data:" + iccCurrencyDOTList.ToString());
            return iccCurrencyDOTList;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = new List<DataTable>();
            dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            txtField = base.parseToView(logKey, logID, projectKey, recValue);

            DataTable iccRecDt = this.getDescription();

            if (dts[0].Rows.Count > 0)
                for (int rowNum = 0; rowNum < dts[0].Rows.Count; rowNum++)
                {
                    // Configuration Data
                    txtField += System.Environment.NewLine + "Configuration Data Parsing: " + System.Environment.NewLine;

                    for (int colNum = 3; colNum < dts[0].Columns.Count - 1; colNum++)
                        txtField += App.Prj.getOptionDescription(iccRecDt, "1" + colNum.ToString("00"), dts[0].Rows[rowNum][colNum].ToString());
                }
            return txtField;
        }
    }
}
