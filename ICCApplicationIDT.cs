using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logger
{

    class ICCApplicationIDT : EMVConfiguration, IMessage
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

                List<iccApplication> iccApplicationList = parseData(tmpTypes[3]);
                // write Language Support Transaction

                foreach (iccApplication c in iccApplicationList)
                {
                    //todo: Implement writting to database.

                    //string sql = @"INSERT INTO ICCApplicationIDT([logkey],[languageCode],[screenBase],[audioBase],
	                   //         [opCodeBufferPositions],[opCodeBufferValues],[logID]) " +
                    //  " VALUES('" + r.typeIndex + "','" + c.LanguageCode + "','" + c.ScreenBase + "','" +
                    //            c.AudioBase + "','" + c.OpCodeBufferPositions + "','" + c.OpCodeBufferValues + "'," +
                    //            logID + ")";

                    //DbCrud db = new DbCrud();
                    //if (db.addToDb(sql) == false)
                    //    return false;
                }
                List<typeRec> emvList = new List<typeRec>();
                emvList.Add(r);
                if (base.writeData(emvList, Key, logID) == false)
                    return false;
            }
            return true;
        }
        public new List<iccApplication> parseData(string tmpTypes)
        {
            iccApplication iccApp = new iccApplication();
            List<iccApplication> iccAppList = new List<iccApplication>();

            
            string[] tmpAids = tmpTypes.Split((char)0x1d);
            int hexLength = 0;
            for (int x = 0; x < tmpAids.Length; x++)
            {
                int offset = 0;
                iccApp.EntryNumber = tmpAids[x].Substring(offset, 2);
                offset += 2;
                iccApp.PrimaryAIDLength = tmpAids[x].Substring(offset, 2);
                offset += 2;
                hexLength = Convert.ToInt32(iccApp.PrimaryAIDLength, 16);
                iccApp.PrimaryAIDValue = tmpAids[x].Substring(offset, hexLength * 2);
                offset += hexLength * 2;
                iccApp.DefaultAppLabelLength = tmpAids[x].Substring(offset, 2);
                offset += 2;
                // joe
                hexLength = Convert.ToInt32(iccApp.DefaultAppLabelLength, 16);
                if (hexLength > 0)
                {
                    iccApp.DefaultAppValue = tmpAids[x].Substring(offset, hexLength * 2);
                    offset += hexLength * 2;
                }
                iccApp.PrimaryAIDICCAppType = tmpAids[x].Substring(offset, 3);
                offset += 3;
                iccApp.PrimaryAIDLowestAppVersion = tmpAids[x].Substring(offset, 4);
                offset += 4;
                iccApp.PrimaryAIDHighestAppVersion = tmpAids[x].Substring(offset, 4);
                offset += 4;
                iccApp.PrimaryAIDActionCode = tmpAids[x].Substring(offset, 10);
                offset += 10;
                iccApp.NumberOfDataObjectTReq = tmpAids[x].Substring(offset, 2);
                offset += 2;
                // TLV
                hexLength = Convert.ToInt32(iccApp.NumberOfDataObjectTReq, 16);
                iccApp.DataObjectForTReq = iccTLVTags(tmpAids[x].Substring(offset, tmpAids[x].Length - offset), hexLength);
                offset += iccApp.DataObjectForTReq.Length - hexLength;


                iccApp.NumberOfDataObjectCompletion = tmpAids[x].Substring(offset, 2);
                offset += 2;
                // TLV
                hexLength = Convert.ToInt32(iccApp.NumberOfDataObjectCompletion, 16);

                if (hexLength > 0)
                {
                    iccApp.DataObjectForCompletion = iccTLVTags(tmpAids[x].Substring(offset, tmpAids[x].Length - offset), hexLength * 2);
                    offset += iccApp.DataObjectForCompletion.Length - (hexLength*2);
                }

                iccApp.NumberOfSecondaryAID = tmpAids[x].Substring(offset, 2);
                offset += 2;
                hexLength = Convert.ToInt32(iccApp.NumberOfSecondaryAID, 16);
                if ( hexLength > 0)
                {
                    iccApp.SecondaryAIDLength = tmpAids[x].Substring(offset, 2);
                    offset += 2;
                    hexLength = Convert.ToInt32(iccApp.SecondaryAIDLength, 16);
                    iccApp.SecondaryAIDValue = tmpAids[x].Substring(offset, hexLength * 2);
                    offset += hexLength * 2;
                }

                if (offset < tmpAids[x].Length)
                {
                    iccApp.AppSelectionIndicator = tmpAids[x].Substring(offset, 2);
                    offset += 2;
                }
                if (offset < tmpAids[x].Length)
                {
                    iccApp.Trk2DataForCentral = tmpAids[x].Substring(offset, 2);
                    offset += 2;
                }
                if (offset < tmpAids[x].Length)
                {
                    iccApp.Trk2DataUsedDuringICCTransaction = tmpAids[x].Substring(offset, 2);
                    offset += 2;
                }
                if (offset < tmpAids[x].Length)
                {
                    iccApp.AdditionalTrk2DataLength = tmpAids[x].Substring(offset, 2);
                    offset += 2;
                    hexLength = Convert.ToInt32(iccApp.AdditionalTrk2DataLength, 16);
                    iccApp.AdditionalTrk2Data = tmpAids[x].Substring(offset, hexLength * 2);
                    offset += hexLength * 2;
                }
                iccAppList.Add(iccApp);
            }

            return iccAppList;
        }

        private string iccTLVTags(string strTag, int tagsNumber)
        {
            // what tags?
            string tags = "";
            int offset = 0;
            for (int x = 0; x < tagsNumber; x++)
            {
                if (emvTags.Contains("," + strTag.Substring(offset, 2) + ","))
                {
                    tags += strTag.Substring(offset, 2) + " ";
                    offset += 2;
                }
                else
                {
                    tags += strTag.Substring(offset, 4) + " ";
                    offset += 4;
                }
            }
            return tags;
        }
    }

}

