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
            DataTable dt = new DataTable();
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = '8' and subRecType like '5%'";

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
            sql = @"SELECT * from ICCApplicationIDT WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);
            return dts;
        }

        public new bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                string[] tmpTypes = r.typeContent.Split((char)0x1c);

                List<iccApplication> iccApplicationList = parseData(tmpTypes[3]);
                // write Language Support Transaction
                int entries = 0;
                foreach (iccApplication c in iccApplicationList)
                {
                    // Implement writting to database.
                    entries += 1;
                    string sql = @"INSERT INTO ICCApplicationIDT([logkey],[rectype],[entryNumber],[primaryAIDLength],
                                [primaryAIDValue],[defaultAppLabelLength],[defaultAppValue],[primaryAIDICCAppType],
	                            [primaryAIDLowestAppVersion],[primaryAIDHighestAppVersion],[primaryAIDActionCode],
	                            [numberOfDataObjectTReq],[dataObjectForTReq],[numberOfDataObjectCompletion],
                                [dataObjectForCompletion],[numberOfSecondaryAID],[secondaryAIDLgthValue],
                                [appSelectionIndicator],[trk2DataForCentral],[trk2DataUsedDuringICCTransaction],
                                [additionalTrk2DataLength],[additionalTrk2Data],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + c.Rectype + "','" + c.EntryNumber + "','" + c.PrimaryAIDLength + "','" +
                                c.PrimaryAIDValue + "','" + c.DefaultAppLabelLength + "','" + c.DefaultAppValue + "','" +
                                c.PrimaryAIDICCAppType + "','" + c.PrimaryAIDLowestAppVersion + "','" + 
                                c.PrimaryAIDHighestAppVersion + "','" + c.PrimaryAIDActionCode + "','" + 
                                c.NumberOfDataObjectTReq + "','" + c.DataObjectForTReq + "','" + c.NumberOfDataObjectCompletion + "','" +
                                c.DataObjectForCompletion + "','" + c.NumberOfSecondaryAID + "','" +
                                c.SecondaryAIDLgthValue + "','" + c.AppSelectionIndicator + "','" + c.Trk2DataForCentral + "','" +
                                c.Trk2DataUsedDuringICCTransaction + "','" + c.AdditionalTrk2DataLength + "','" +
                                c.AdditionalTrk2Data + "'," + logID + ")";

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
        public new List<iccApplication> parseData(string tmpTypes)
        {
            iccApplication iccApp = new iccApplication();
            List<iccApplication> iccAppList = new List<iccApplication>();

            
            string[] tmpAids = tmpTypes.Split((char)0x1d);
            int hexLength = 0;
            for (int x = 0; x < tmpAids.Length; x++)
            {
                iccApp.Rectype = "85";
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
                    iccApp.DefaultAppValue = tmpAids[x].Substring(offset, hexLength);
                    offset += hexLength;
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
                    iccApp.DataObjectForCompletion = iccTLVTags(tmpAids[x].Substring(offset, tmpAids[x].Length - offset), hexLength);
                    offset += iccApp.DataObjectForCompletion.Length - hexLength;
                }

                iccApp.NumberOfSecondaryAID = tmpAids[x].Substring(offset, 2);
                offset += 2;
                hexLength = Convert.ToInt32(iccApp.NumberOfSecondaryAID, 16);
                iccApp.SecondaryAIDLgthValue = "";
                for (int y = 0; y < hexLength; y++)
                {
                    iccApp.SecondaryAIDLgthValue += tmpAids[x].Substring(offset, 2) + " ";
                    int hexLength2 = Convert.ToInt32(tmpAids[x].Substring(offset, 2), 16);
                    offset += 2;
                    iccApp.SecondaryAIDLgthValue += tmpAids[x].Substring(offset, hexLength2 * 2) + " ";
                    offset += hexLength2 * 2;
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

