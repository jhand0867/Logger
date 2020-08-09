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
            DataTable dt = new DataTable();
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = '8' and subRecType like '4%'";

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
            sql = @"SELECT * from ICCTerminalDOT WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);
            return dts;
        }

        public new bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                string[] tmpTypes = r.typeContent.Split((char)0x1c);

                List<iccTerminal> iccTerminalList = parseData(tmpTypes[3]);
                // write Language Support Transaction
                int entries = 0;
                foreach (iccTerminal c in iccTerminalList)
                {
                    entries += 1;
                    string sql = @"INSERT INTO ICCTerminalDOT([logkey],[rectype],[responseFormat],[responseLength],[terCountryCodeTag],
                                	[terCountryCodeLgth],[terCountryCodeValue],[terTypeTag],[terTypeLgth],[terTypeValue],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + c.Rectype + "','" + c.ResponseFormat + "','" + c.ResponseLength + "','" +
                                c.TerCountryCodeTag + "','" + c.TerCountryCodeLgth + "','" + c.TerCountryCodeValue + "','" +
                                c.TerTypeTag + "','" + c.TerTypeLgth + "','" + c.TerTypeValue + "'," +
                                logID + ")";

                    DbCrud db = new DbCrud();
                    if (db.addToDb(sql) == false)
                        return false;
                }
                List<typeRec> emvList = new List<typeRec>();
                typeRec rec = r;
                rec.typeAddData = entries.ToString();
                emvList.Add(rec);
                if (base.writeData(emvList, Key, logID) == false)
                    return false;
            }
            return true;
        }
        public new List<iccTerminal> parseData(string tmpTypes)
        {
            iccTerminal iccTerminal = new iccTerminal();
            List<iccTerminal> iccTerminalList = new List<iccTerminal>();

            iccTerminal.Rectype = "84";
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
