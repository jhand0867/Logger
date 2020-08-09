using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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

            string sql = @"SELECT TOP 1 * from EMVConfiguration WHERE logID = '" + logID + "' AND prjkey = '" + projectKey + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            // dt = new DataTable();
            sql = @"SELECT * from ICCTransactionDOT WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
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
    }
}
