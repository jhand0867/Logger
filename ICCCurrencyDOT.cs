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
    struct iccCurrency
    {
        public string currencyType;
        public string responseFormat;
        public string responseLength;
        public string trCurrencyCodeTag;
        public string trCurrencyCodeLgth;
        public string trCurrencyCodeValue;
        public string trCurrencyExpTag;
        public string trCurrencyExpLgth;
        public string trCurrencyExpValue;
    }

    struct iccTransaction
    {

    };

    struct emvConfiguration
    {
        public string rectype;
        public string responseFlag;
        public string luno;
        public string msgSubclass;
        public string numberOfEntries;
        public List<iccCurrency> iccCurrencyDOTList;
        public List<iccTransaction> iccTransactionDOTList;    
        public string configurationData;
        public string mac;
    };
    class ICCCurrencyDOT : App, IMessage
    {
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

                    emvConfiguration emvC = new emvConfiguration();
                    emvC.rectype = "8";
                    emvC.responseFlag = "";
                    emvC.luno = tmpTypes[1];
                    emvC.msgSubclass = tmpTypes[2];
                    emvC.numberOfEntries = tmpTypes[3].Substring(0, 2);
                    emvC.configurationData = tmpTypes[3];
                    emvC.mac = tmpTypes[4];

                    emvC.iccCurrencyDOTList = new List<iccCurrency>();
                    iccCurrency iccCurrency = new iccCurrency();
                    int offset = 2;
                    for (int x = 0; x < int.Parse(emvC.numberOfEntries); x++)
                    {
                        iccCurrency.currencyType = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccCurrency.responseFormat = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccCurrency.responseLength = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccCurrency.trCurrencyCodeTag = tmpTypes[3].Substring(offset, 4);
                        offset += 4;
                        iccCurrency.trCurrencyCodeLgth = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccCurrency.trCurrencyCodeValue = tmpTypes[3].Substring(offset, 
                                             int.Parse(iccCurrency.trCurrencyCodeLgth)*2);
                        offset += int.Parse(iccCurrency.trCurrencyCodeLgth) * 2;
                        iccCurrency.trCurrencyExpTag = tmpTypes[3].Substring(offset, 4);
                        offset += 4;
                        iccCurrency.trCurrencyExpLgth = tmpTypes[3].Substring(offset, 2);
                        offset += 2;
                        iccCurrency.trCurrencyExpValue = tmpTypes[3].Substring(offset,
                                             int.Parse(iccCurrency.trCurrencyExpLgth) * 2);
                        offset += int.Parse(iccCurrency.trCurrencyExpLgth) * 2;

                        emvC.iccCurrencyDOTList.Add(iccCurrency);
                    }
                   


                    sql = @"INSERT INTO EMVConfiguration([logkey],[rectype],[responseFlag],
	                        [luno],[msgSubclass],[numberOfEntries],[configurationData],[mac],[prjkey],[logID]) " +
                          " VALUES('" + r.typeIndex + "','" + emvC.rectype + "','" + emvC.responseFlag + "','" +
                                   emvC.luno + "','" + emvC.msgSubclass + "','" + emvC.numberOfEntries + "','" +
                                   emvC.configurationData + "','" + emvC.mac + "','" + Key + "'," + logID + ")";

                    command = new SqlCommand(sql, cnn);
                    dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                    dataAdapter.InsertCommand.ExecuteNonQuery();
                    command.Dispose();

                    // write currency DOT

                    foreach (iccCurrency c in emvC.iccCurrencyDOTList)
                    {

                        sql = @"INSERT INTO ICCCurrencyDOT([logkey],[currencyType],[responseFormat],[responseLength],
	                            [trCurrencyCodeTag],[trCurrencyCodeLgth],[trCurrencyCodeValue],[trCurrencyExpTag],
	                            [trCurrencyExpLgth],[trCurrencyExpValue],[logID]) " +
                          " VALUES('" + r.typeIndex + "','" + c.currencyType + "','" + c.responseFormat + "','" +
                                    c.responseLength + "','" + c.trCurrencyCodeTag + "','" + c.trCurrencyCodeLgth + "','" +
                                    c.trCurrencyCodeValue + "','" + c.trCurrencyExpTag + "','" + c.trCurrencyExpLgth + "','" +
                                    c.trCurrencyExpValue + "'," + logID + ")";

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
