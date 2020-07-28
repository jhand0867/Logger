using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logger
{

    class ICCLanguageSupportT : EMVConfiguration, IMessage
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
            foreach (typeRec r in typeRecs)
            {
                string[] tmpTypes = r.typeContent.Split((char)0x1c);

                iccLanguage iccLanguage = new iccLanguage();
                List<iccLanguage> iccLanguageList = new List<iccLanguage>();

                int offset = 2;
                for (int x = 0; x < int.Parse(tmpTypes[3].Substring(0, 2)); x++)
                {
                    iccLanguage.LanguageCode = tmpTypes[3].Substring(offset, 2);
                    offset += 2;
                    iccLanguage.ScreenBase = tmpTypes[3].Substring(offset, 3);
                    offset += 3;
                    iccLanguage.AudioBase = tmpTypes[3].Substring(offset, 3);
                    offset += 3;
                    iccLanguage.OpCodeBufferPositions = tmpTypes[3].Substring(offset, 3);
                    offset += 3;
                    iccLanguage.OpCodeBufferValues = tmpTypes[3].Substring(offset, 3);
                    offset += 3;
                    iccLanguageList.Add(iccLanguage);
                }
                // write Language Support Transaction

                foreach (iccLanguage c in iccLanguageList)
                {

                    string sql = @"INSERT INTO ICCLanguageSupportT([logkey],[languageCode],[screenBase],[audioBase],
	                            [opCodeBufferPositions],[opCodeBufferValues],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + c.LanguageCode + "','" + c.ScreenBase + "','" +
                                c.AudioBase + "','" + c.OpCodeBufferPositions + "','" + c.OpCodeBufferValues + "'," +
                                logID + ")";

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
    }

}
