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

            string sql = @"SELECT TOP 1 * from EMVConfiguration WHERE logID = '" + logID + "' AND prjkey = '" + projectKey + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            // dt = new DataTable();
            sql = @"SELECT * from ICCCurrencyDOT WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);
            return dts;

        }

        public new bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
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
                    if (db.addToDb(sql) == false)
                        return false;
                }

                //EMVConfiguration emv = new EMVConfiguration();
                List<typeRec> emvList = new List<typeRec>();
                emvList.Add(r);
                if (base.writeData(emvList, Key, logID) == false)
                    return false;
            }
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

            // EMVConfiguration emvRec = new EMVConfiguration();
            DataTable emvRecDt = base.getDescription();
            DataTable iccRecDt = this.getDescription();

            foreach (DataTable dt in dts)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                    {
                        if (dt.Rows[rowNum][2].ToString() == "8")
                        {
                            // message class
                            txtField += emvRecDt.Rows[0][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][2].ToString().Trim() + System.Environment.NewLine;

                            // response flag
                            txtField += emvRecDt.Rows[1][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][3].ToString().Trim() + System.Environment.NewLine;

                            // luno
                            txtField += emvRecDt.Rows[2][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][4].ToString().Trim() + System.Environment.NewLine;

                            // message subclass
                            txtField += emvRecDt.Rows[3][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][5].ToString().Trim() + System.Environment.NewLine;

                            // configuration data
                            txtField += emvRecDt.Rows[4][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][7].ToString().Trim() + System.Environment.NewLine;


                            // MAC
                            txtField += emvRecDt.Rows[5][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][8].ToString().Trim() + System.Environment.NewLine;
                        }
                        if (dt.Rows[rowNum][2].ToString() == "81")
                        {
                            // Configuration Data
                            txtField += System.Environment.NewLine + "Configuration Data Parsing: " + System.Environment.NewLine;

                            // currency type
                            txtField += iccRecDt.Rows[1][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][3].ToString().Trim() + System.Environment.NewLine;

                            // Response format tag
                            txtField += iccRecDt.Rows[2][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][4].ToString().Trim() + System.Environment.NewLine;

                            // Response format length
                            txtField += iccRecDt.Rows[3][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][5].ToString().Trim() + System.Environment.NewLine;

                            // Transaction Currency Code Tag
                            txtField += iccRecDt.Rows[4][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][6].ToString().Trim() + System.Environment.NewLine;

                            // Transaction Currency Code lgth
                            txtField += iccRecDt.Rows[5][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][7].ToString().Trim() + System.Environment.NewLine;

                            // Transaction Currency Code value
                            txtField += iccRecDt.Rows[6][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][8].ToString().Trim() + System.Environment.NewLine;

                            // Transaction Currency Exp Tag
                            txtField += iccRecDt.Rows[7][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][9].ToString().Trim() + System.Environment.NewLine;

                            // Transaction Currency Exp lgth
                            txtField += iccRecDt.Rows[8][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][10].ToString().Trim() + System.Environment.NewLine;

                            // Transaction Currency Exp value
                            txtField += iccRecDt.Rows[9][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][11].ToString().Trim() + System.Environment.NewLine;

                        }
                    }
                }
            }

            return txtField;
        }
    }
}
