using System.Collections.Generic;
using System.Data;

namespace Logger
{

    class ICCLanguageSupportT : EMVConfiguration, IMessage
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public new DataTable getDescription()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = '8' and subRecType like '3%'";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            return dt;
        }

        public new List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * from ICCLanguageSupportT WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
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

                List<iccLanguage> iccLanguageList = parseData(tmpTypes[3]);
                // write Language Support Transaction

                foreach (iccLanguage c in iccLanguageList)
                {

                    string sql = @"INSERT INTO ICCLanguageSupportT([logkey],[rectype],[languageCode],[screenBase],[audioBase],
	                            [opCodeBufferPositions],[opCodeBufferValues],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + c.Rectype + "','" + c.LanguageCode + "','" + c.ScreenBase + "','" +
                                c.AudioBase + "','" + c.OpCodeBufferPositions + "','" + c.OpCodeBufferValues + "'," +
                                logID + ")";

                    DbCrud db = new DbCrud();
                    if (db.crudToDb(sql) == false)
                        return false;
                }
                List<typeRec> emvList = new List<typeRec>();
                emvList.Add(r);
                if (base.writeData(emvList, Key, logID) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;
        }
        public new List<iccLanguage> parseData(string tmpTypes)
        {
            iccLanguage iccLanguage = new iccLanguage();
            List<iccLanguage> iccLanguageList = new List<iccLanguage>();

            int offset = 2;
            for (int x = 0; x < int.Parse(tmpTypes.Substring(0, 2)); x++)
            {
                iccLanguage.Rectype = "83";
                iccLanguage.LanguageCode = tmpTypes.Substring(offset, 2);
                offset += 2;
                iccLanguage.ScreenBase = tmpTypes.Substring(offset, 3);
                offset += 3;
                iccLanguage.AudioBase = tmpTypes.Substring(offset, 3);
                offset += 3;
                iccLanguage.OpCodeBufferPositions = tmpTypes.Substring(offset, 3);
                offset += 3;
                iccLanguage.OpCodeBufferValues = tmpTypes.Substring(offset, 3);
                offset += 3;
                iccLanguageList.Add(iccLanguage);
            }
            return iccLanguageList;
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
                        txtField += App.Prj.getOptionDescription(iccRecDt, "3" + colNum.ToString("00"), dts[0].Rows[rowNum][colNum].ToString());
                }
            return txtField;
        }
    }

}
