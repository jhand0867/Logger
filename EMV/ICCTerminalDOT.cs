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
            foreach (typeRec r in typeRecs)
            {
                string[] tmpTypes = r.typeContent.Split((char)0x1c);

                List<iccTerminal> iccTerminalList = parseData(tmpTypes[3]);
                // write Language Support Transaction
                int entries = 0;
                foreach (iccTerminal c in iccTerminalList)
                {
                    entries += 1;
                    string sql = @"INSERT INTO ICCTerminalDOT([logkey],[rectype],[responseFormat],[responseLength],[terCountryCodeTag],
                                	[terCountryCodeLgth],[terCountryCodeValue],[terTypeTag],[terTypeLgth],[terTypeValue],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + c.Rectype + "','" + c.ResponseFormat + "','" + c.ResponseLength + "','" +
                                c.TerCountryCodeTag + "','" + c.TerCountryCodeLgth + "','" + c.TerCountryCodeValue + "','" +
                                c.TerTypeTag + "','" + c.TerTypeLgth + "','" + c.TerTypeValue + "'," +
                                logID + ")";

                    DbCrud db = new DbCrud();
                    if (db.addToDb(sql) == false)
                        return false;
                }
                List<typeRec> emvList = new List<typeRec>();
                typeRec rec = r;
                rec.typeAddData = entries.ToString();
                emvList.Add(rec);
                if (base.writeData(emvList, Key, logID) == false)
                    return false;
            }
            return true;
        }
        public new List<iccTerminal> parseData(string tmpTypes)
        {
            iccTerminal iccTerminal = new iccTerminal();
            List<iccTerminal> iccTerminalList = new List<iccTerminal>();

            iccTerminal.Rectype = "84";
            int offset = 0;
            iccTerminal.ResponseFormat = tmpTypes.Substring(offset, 2);
            offset += 2;
            iccTerminal.ResponseLength = tmpTypes.Substring(offset, 2);
            offset += 2;
            iccTerminal.TerCountryCodeTag = tmpTypes.Substring(offset, 4);
            offset += 4;
            iccTerminal.TerCountryCodeLgth = tmpTypes.Substring(offset, 2);
            offset += 2;
            iccTerminal.TerCountryCodeValue = tmpTypes.Substring(offset, int.Parse(iccTerminal.TerCountryCodeLgth) * 2);
            offset += int.Parse(iccTerminal.TerCountryCodeLgth) * 2;
            iccTerminal.TerTypeTag = tmpTypes.Substring(offset, 4);
            offset += 4;
            iccTerminal.TerTypeLgth = tmpTypes.Substring(offset, 2);
            offset += 2;
            iccTerminal.TerTypeValue = tmpTypes.Substring(offset, int.Parse(iccTerminal.TerTypeLgth) * 2);
            offset += int.Parse(iccTerminal.TerTypeLgth) * 2;

            iccTerminalList.Add(iccTerminal);
            return iccTerminalList;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = new List<DataTable>();
            dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            txtField = base.parseToView(logKey, logID, projectKey, recValue);

            DataTable iccRecDt = this.getDescription();

            foreach (DataTable dt in dts)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                    {

                            // Configuration Data
                            txtField += System.Environment.NewLine + "Configuration Data Parsing: " + System.Environment.NewLine;

                            // Response format tag
                            txtField += iccRecDt.Rows[0][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][3].ToString().Trim() + System.Environment.NewLine;

                            // Response format length
                            txtField += iccRecDt.Rows[1][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][4].ToString().Trim() + System.Environment.NewLine;

                            //  Tag
                            txtField += iccRecDt.Rows[2][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][5].ToString().Trim() + System.Environment.NewLine;

                            //  lgth
                            txtField += iccRecDt.Rows[3][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][6].ToString().Trim() + System.Environment.NewLine;

                            //  value
                            txtField += iccRecDt.Rows[4][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][7].ToString().Trim() + System.Environment.NewLine;

                            // Tag
                            txtField += iccRecDt.Rows[5][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][8].ToString().Trim() + System.Environment.NewLine;

                            // lgth
                            txtField += iccRecDt.Rows[6][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][9].ToString().Trim() + System.Environment.NewLine;

                            // value
                            txtField += iccRecDt.Rows[7][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][10].ToString().Trim() + System.Environment.NewLine;
                    }
                }
            }

            return txtField;
        }
    }

}
