using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logger
{
    class ICCCurrencyDOT: EMVConfiguration, IMessage
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public new DataTable getDescription()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = '8' and subRecType like '1%'";

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
            sql = @"SELECT * from ICCCurrencyDOT WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);
            return dts;

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
                    string sql = @"INSERT INTO ICCCurrencyDOT([logkey],[rectype],[currencyType],[responseFormat],[responseLength],
	                            [trCurrencyCodeTag],[trCurrencyCodeLgth],[trCurrencyCodeValue],[trCurrencyExpTag],
	                            [trCurrencyExpLgth],[trCurrencyExpValue],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + c.Rectype + "','" + c.CurrencyType + "','" + c.ResponseFormat + "','" +
                                c.ResponseLength + "','" + c.TrCurrencyCodeTag + "','" + c.TrCurrencyCodeLgth + "','" +
                                c.TrCurrencyCodeValue + "','" + c.TrCurrencyExpTag + "','" + c.TrCurrencyExpLgth + "','" +
                                c.TrCurrencyExpValue + "'," + logID + ")";

                    DbCrud db = new DbCrud();
                    if (db.addToDb(sql) == false)
                        return false;
                }
                
                //EMVConfiguration emv = new EMVConfiguration();
                List<typeRec> emvList = new List<typeRec>();
                emvList.Add(r);
                if (base.writeData(emvList, Key, logID) == false)
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
                iccCurrency.Rectype = "81";
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
