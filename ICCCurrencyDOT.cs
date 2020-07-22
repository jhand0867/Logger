using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
     class ICCCurrencyDOT : EMVConfiguration, IMessage
    {
        private emvConfiguration emv = new emvConfiguration();
        public ICCCurrencyDOT(emvConfiguration emvConfiguration)
        {
            Emv = emvConfiguration;
        }

        public emvConfiguration Emv { get => emv; set => emv = value; }

        public DataTable getDescription()
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

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
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

        // get 4's from database

        public bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                String sql = "";

                string[] tmpTypes;

                foreach (typeRec r in typeRecs)
                {
                    tmpTypes = r.typeContent.Split((char)0x1c);

                    emv.Rectype = "8";
                    emv.ResponseFlag = "";
                    emv.Luno = tmpTypes[1];
                    emv.MsgSubclass = tmpTypes[2];
                    emv.NumberOfEntries = tmpTypes[3].Substring(0, 2);
                    emv.ConfigurationData = tmpTypes[3];
                    emv.Mac = tmpTypes[4];

                    emv.IccCurrencyDOTList = new List<iccCurrency>();
                    iccCurrency iccCurrency = new iccCurrency();
                    int offset = 2;
                    for (int x = 0; x < int.Parse(emv.NumberOfEntries); x++)
                    {
                        iccCurrency.CurrencyType = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccCurrency.ResponseFormat = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccCurrency.ResponseLength = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccCurrency.TrCurrencyCodeTag = tmpTypes[3].Substring(offset, 4);
                        offset += 4;
                        iccCurrency.TrCurrencyCodeLgth = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccCurrency.TrCurrencyCodeValue = tmpTypes[3].Substring(offset, 
                                             int.Parse(iccCurrency.TrCurrencyCodeLgth)*2);
                        offset += int.Parse(iccCurrency.TrCurrencyCodeLgth) * 2;
                        iccCurrency.TrCurrencyExpTag = tmpTypes[3].Substring(offset, 4);
                        offset += 4;
                        iccCurrency.TrCurrencyExpLgth = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccCurrency.TrCurrencyExpValue = tmpTypes[3].Substring(offset,
                                             int.Parse(iccCurrency.TrCurrencyExpLgth) * 2);
                        offset += int.Parse(iccCurrency.TrCurrencyExpLgth) * 2;

                        emv.IccCurrencyDOTList.Add(iccCurrency);
                    }
                   


                    sql = @"INSERT INTO EMVConfiguration([logkey],[rectype],[responseFlag],
	                        [luno],[msgSubclass],[numberOfEntries],[configurationData],[mac],[prjkey],[logID]) " +
                          " VALUES('" + r.typeIndex + "','" + emv.Rectype + "','" + emv.ResponseFlag + "','" +
                                   emv.Luno + "','" + emv.MsgSubclass + "','" + emv.NumberOfEntries + "','" +
                                   emv.ConfigurationData + "','" + emv.Mac + "','" + Key + "'," + logID + ")";

                    command = new SqlCommand(sql, cnn);
                    dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                    dataAdapter.InsertCommand.ExecuteNonQuery();
                    command.Dispose();

                    // write currency DOT

                    foreach (iccCurrency c in emv.IccCurrencyDOTList)
                    {

                        sql = @"INSERT INTO ICCCurrencyDOT([logkey],[currencyType],[responseFormat],[responseLength],
	                            [trCurrencyCodeTag],[trCurrencyCodeLgth],[trCurrencyCodeValue],[trCurrencyExpTag],
	                            [trCurrencyExpLgth],[trCurrencyExpValue],[logID]) " +
                          " VALUES('" + r.typeIndex + "','" + c.CurrencyType + "','" + c.ResponseFormat + "','" +
                                    c.ResponseLength + "','" + c.TrCurrencyCodeTag + "','" + c.TrCurrencyCodeLgth + "','" +
                                    c.TrCurrencyCodeValue + "','" + c.TrCurrencyExpTag + "','" + c.TrCurrencyExpLgth + "','" +
                                    c.TrCurrencyExpValue + "'," + logID + ")";

                        command = new SqlCommand(sql, cnn);
                        dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                        dataAdapter.InsertCommand.ExecuteNonQuery();
                        command.Dispose();
                    }

                }
                cnn.Close();
                return true;
            }

            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());
                return false;

            }

        }
    }
}
