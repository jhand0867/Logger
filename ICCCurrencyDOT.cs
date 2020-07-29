using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logger
{
    class ICCCurrencyDOT : EMVConfiguration, IMessage
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // private emvConfiguration emv = new emvConfiguration();        


        //public ICCCurrencyDOT(emvConfiguration emvConfiguration)
        //{
        //    this.emv = emvConfiguration;
        //}

        //internal emvConfiguration Emv { get => emv; set => emv = value; }

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
                log.Error("Database Error: " + dbEx.ToString());
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
                log.Error("Database Error: " + dbEx.ToString());
                return null;
            }
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
                    string sql = @"INSERT INTO ICCCurrencyDOT([logkey],[currencyType],[responseFormat],[responseLength],
	                            [trCurrencyCodeTag],[trCurrencyCodeLgth],[trCurrencyCodeValue],[trCurrencyExpTag],
	                            [trCurrencyExpLgth],[trCurrencyExpValue],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + c.CurrencyType + "','" + c.ResponseFormat + "','" +
                                c.ResponseLength + "','" + c.TrCurrencyCodeTag + "','" + c.TrCurrencyCodeLgth + "','" +
                                c.TrCurrencyCodeValue + "','" + c.TrCurrencyExpTag + "','" + c.TrCurrencyExpLgth + "','" +
                                c.TrCurrencyExpValue + "'," + logID + ")";

                    DbCrud db = new DbCrud();
                    if (db.addToDb(sql) == false)
                        return false;
                }
                EMVConfiguration emv = new EMVConfiguration();
                List<typeRec> emvList = new List<typeRec>();
                emvList.Add(r);
                if (emv.writeData(emvList, Key, logID) == false)
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
    }
}
