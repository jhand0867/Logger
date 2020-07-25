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
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            DataTable dt = new DataTable();


            // is the calling state a Y
            // get the info from DataDescription
            // send it back in a string 
            try
            {
                cnn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [dataDescription] WHERE recType = '" + "8" + "'", cnn))
                {
                    sda.Fill(dt);
                }
            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());
                return null;
            }

            return dt; ;
        }

        public new List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            List<DataTable> dts = new List<DataTable>();
            DataTable dt = new DataTable();

            try
            {
                cnn.Open();

                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT TOP 1 * from EMVConfiguration WHERE logID = '" + logID + "' AND prjkey = '" + projectKey + "' AND logkey LIKE '" + logKey + "%'", cnn))
                {
                    sda.Fill(dt);
                }
                dts.Add(dt);
                dt = new DataTable();
                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * from ICCCurrencyDOT WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'", cnn))
                {
                    sda.Fill(dt);
                }
                dts.Add(dt);
                return dts;
            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());
                return null;
            }
        }

        public new bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            log.Info("Adding to Database ");
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                string[] tmpTypes;

                foreach (typeRec r in typeRecs)
                {
                    tmpTypes = r.typeContent.Split((char)0x1c);

                    iccTransaction iccTransaction = new iccTransaction();
                    List<iccTransaction> iccTransactionDOTList = new List<iccTransaction>();

                    int offset = 2;
                    for (int x = 0; x < int.Parse(tmpTypes[3].Substring(0, 2)); x++)
                    {
                        iccTransaction.TransactionType = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccTransaction.ResponseFormat = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccTransaction.ResponseLength = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccTransaction.TransactionTypeTag = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccTransaction.TransactionTypeLgth = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccTransaction.TransactionTypeValue = tmpTypes[3].Substring(offset,
                                             int.Parse(iccTransaction.TransactionTypeLgth) * 2);
                        offset += int.Parse(iccTransaction.TransactionTypeLgth) * 2;
                        iccTransaction.TransactionCatCodeTag = tmpTypes[3].Substring(offset, 4);
                        offset += 4;
                        iccTransaction.TransactionCatCodeLgth = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccTransaction.TransactionCatCodeValue = tmpTypes[3].Substring(offset,
                                             int.Parse(iccTransaction.TransactionCatCodeLgth) * 2);
                        offset += int.Parse(iccTransaction.TransactionCatCodeLgth) * 2;

                        iccTransactionDOTList.Add(iccTransaction);
                    }
                    // write Transaction DOT

                    foreach (iccTransaction c in iccTransactionDOTList)
                    {

                        string sql = @"INSERT INTO ICCTransactionDOT([logkey],[transactionType],[responseFormat],[responseLength],
	                            [transactionTypeTag],[transactionTypeLgth],[transactionTypeValue],[transactionCatCodeTag],
	                            [transactionCatCodeLgth],[transactionCatCodeValue],[logID]) " +
                          " VALUES('" + r.typeIndex + "','" + c.TransactionType + "','" + c.ResponseFormat + "','" +
                                    c.ResponseLength + "','" + c.TransactionTypeTag + "','" + c.TransactionTypeLgth + "','" +
                                    c.TransactionTypeValue + "','" + c.TransactionCatCodeTag + "','" + c.TransactionCatCodeLgth + "','" +
                                    c.TransactionCatCodeValue + "'," + logID + ")";
                        log.Debug("Adding record " + sql);
                        command = new SqlCommand(sql, cnn);
                        dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                        dataAdapter.InsertCommand.ExecuteNonQuery();
                        command.Dispose();
                        log.Debug("Record Added");
                    }
                    EMVConfiguration emv = new EMVConfiguration();
                    emv.writeData(r, Key, logID);
                }
                cnn.Close();
                return true;
            }

            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return false;

            }

        }
    }
}
