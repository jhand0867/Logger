using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    class ICCTransactionDOT : EMVConfiguration, IMessage
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public new DataTable getDescription()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = '8' and subRecType like '2%'";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            return dt;
        }

        public new List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * from ICCTransactionDOT WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
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

                List<iccTransaction> iccTransactionDOTList = parseData(tmpTypes[3]);


                // write Transaction DOT

                foreach (iccTransaction c in iccTransactionDOTList)
                {

                    string sql = @"INSERT INTO ICCTransactionDOT([logkey],[rectype],[transactionType],[responseFormat2Tag],[responseFormat2Length],
	                            [responseFormat2Value],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + c.Rectype + "','" + c.TransactionType + "','" + c.ResponseFormat2Tag + "','" +
                                c.ResponseFormat2Length + "','" + c.ResponseFormat2Value + "'," + logID + ")";

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

        public new List<iccTransaction> parseData(string tmpTypes)
        {
            iccTransaction iccTransaction = new iccTransaction();
            List<iccTransaction> iccTransactionDOTList = new List<iccTransaction>();

            // to preserve the original format, iccTransaction.ResponseFormat2Length field is stored in its hexadecimal value.
            // ie. field length value "15" is equal to 42 bytes (decimal value 21*2)
            // 
            int offset = 2;
            for (int x = 0; x < int.Parse(tmpTypes.Substring(0, 2)); x++)
            {
                iccTransaction.Rectype = "82";
                iccTransaction.TransactionType = tmpTypes.Substring(offset, 2);
                offset += 2;
                iccTransaction.ResponseFormat2Tag = tmpTypes.Substring(offset, 2);
                offset += 2;
                iccTransaction.ResponseFormat2Length = tmpTypes.Substring(offset, 2);
                int length = Int32.Parse(tmpTypes.Substring(offset, 2), System.Globalization.NumberStyles.HexNumber) * 2;
                offset += 2;
                iccTransaction.ResponseFormat2Value = new Digester().iccTLVTags(tmpTypes.Substring(offset, length));
                iccTransactionDOTList.Add(iccTransaction);
                
                offset += length;
            }
            return iccTransactionDOTList;
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
                        txtField += App.Prj.getOptionDescription(iccRecDt, "2" + colNum.ToString("00"), dts[0].Rows[rowNum][colNum].ToString());
                }

            return txtField;
        }
    }
}
