using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logger
{

    class ICCTerminalDOT : EMVConfiguration
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
                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * from ICCTerminalDOT WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'", cnn))
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

                List<iccTerminal> iccTerminalList = parseData(tmpTypes[3]);
                // write Language Support Transaction

                foreach (iccTerminal c in iccTerminalList)
                {
                    string sql = @"INSERT INTO ICCTerminalDOT([logkey],[responseFormat],[responseLength],[terCountryCodeTag],
                                	[terCountryCodeLgth],[terCountryCodeValue],[terTypeTag],[terTypeLgth],[terTypeValue],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + c.ResponseFormat + "','" + c.ResponseLength + "','" +
                                c.TerCountryCodeTag + "','" + c.TerCountryCodeLgth + "','" + c.TerCountryCodeValue + "','" +
                                c.TerTypeTag + "','" + c.TerTypeLgth + "','" + c.TerTypeValue + "'," +
                                logID + ")";

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
        public new List<iccTerminal> parseData(string tmpTypes)
        {
            iccTerminal iccTerminal = new iccTerminal();
            List<iccTerminal> iccTerminalList = new List<iccTerminal>();

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
    }

}
