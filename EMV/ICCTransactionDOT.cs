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

            foreach (typeRec r in typeRecs)
            {
                string[] tmpTypes = r.typeContent.Split((char)0x1c);

                List<iccTransaction> iccTransactionDOTList = parseData(tmpTypes[3]);


                // write Transaction DOT

                foreach (iccTransaction c in iccTransactionDOTList)
                {

                    string sql = @"INSERT INTO ICCTransactionDOT([logkey],[rectype],[transactionType],[responseFormat],[responseLength],
	                            [transactionTypeTag],[transactionTypeLgth],[transactionTypeValue],[transactionCatCodeTag],
	                            [transactionCatCodeLgth],[transactionCatCodeValue],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + c.Rectype + "','" + c.TransactionType + "','" + c.ResponseFormat + "','" +
                                c.ResponseLength + "','" + c.TransactionTypeTag + "','" + c.TransactionTypeLgth + "','" +
                                c.TransactionTypeValue + "','" + c.TransactionCatCodeTag + "','" + c.TransactionCatCodeLgth + "','" +
                                c.TransactionCatCodeValue + "'," + logID + ")";

                    DbCrud db = new DbCrud();
                    if (db.addToDb(sql) == false)
                        return false;
                }
                List<typeRec> emvList = new List<typeRec>();
                emvList.Add(r);
                if (base.writeData(emvList, Key, logID) == false)
                    return false;
            }
            return true;

        }

        public new List<iccTransaction> parseData(string tmpTypes)
        {
            iccTransaction iccTransaction = new iccTransaction();
            List<iccTransaction> iccTransactionDOTList = new List<iccTransaction>();

            int offset = 2;
            for (int x = 0; x < int.Parse(tmpTypes.Substring(0, 2)); x++)
            {
                iccTransaction.Rectype = "82";
                iccTransaction.TransactionType = tmpTypes.Substring(offset, 2);
                offset += 2;
                iccTransaction.ResponseFormat = tmpTypes.Substring(offset, 2);
                offset += 2;
                iccTransaction.ResponseLength = tmpTypes.Substring(offset, 2);
                offset += 2;
                iccTransaction.TransactionTypeTag = tmpTypes.Substring(offset, 2);
                offset += 2;
                iccTransaction.TransactionTypeLgth = tmpTypes.Substring(offset, 2);
                offset += 2;
                iccTransaction.TransactionTypeValue = tmpTypes.Substring(offset,
                                     int.Parse(iccTransaction.TransactionTypeLgth) * 2);
                offset += int.Parse(iccTransaction.TransactionTypeLgth) * 2;
                iccTransaction.TransactionCatCodeTag = tmpTypes.Substring(offset, 4);
                offset += 4;
                iccTransaction.TransactionCatCodeLgth = tmpTypes.Substring(offset, 2);
                offset += 2;
                iccTransaction.TransactionCatCodeValue = tmpTypes.Substring(offset,
                                     int.Parse(iccTransaction.TransactionCatCodeLgth) * 2);
                offset += int.Parse(iccTransaction.TransactionCatCodeLgth) * 2;

                iccTransactionDOTList.Add(iccTransaction);
            }
            return iccTransactionDOTList;
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

                            // transaction type
                            txtField += iccRecDt.Rows[1][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][3].ToString().Trim() + System.Environment.NewLine;

                            // Response format tag
                            txtField += iccRecDt.Rows[2][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][4].ToString().Trim() + System.Environment.NewLine;

                            // Response format length
                            txtField += iccRecDt.Rows[3][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][5].ToString().Trim() + System.Environment.NewLine;

                            //  Tag
                            txtField += iccRecDt.Rows[4][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][6].ToString().Trim() + System.Environment.NewLine;

                            //  lgth
                            txtField += iccRecDt.Rows[5][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][7].ToString().Trim() + System.Environment.NewLine;

                            //  value
                            txtField += iccRecDt.Rows[6][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][8].ToString().Trim() + System.Environment.NewLine;

                            // Tag
                            txtField += iccRecDt.Rows[7][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][9].ToString().Trim() + System.Environment.NewLine;

                            // lgth
                            txtField += iccRecDt.Rows[8][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][10].ToString().Trim() + System.Environment.NewLine;

                            // value
                            txtField += iccRecDt.Rows[9][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][11].ToString().Trim() + System.Environment.NewLine;
                    }
                }
            }

            return txtField;
        }
    }
}
