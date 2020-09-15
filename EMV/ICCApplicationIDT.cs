using System;
using System.Collections.Generic;
using System.Data;

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

            string sql = @"SELECT * from ICCApplicationIDT WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
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
                    if (db.crudToDb(sql) == false)
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

        //todo: move this to EMVConfiguration
        //todo: Make ICCApplicationIDT work!

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

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = new List<DataTable>();
            dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            txtField = base.parseToView(logKey, logID, projectKey, recValue);

            DataTable iccRecDt = this.getDescription();

            foreach (DataTable dt in dts)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                    {
                            // Configuration Data
                            txtField += System.Environment.NewLine + "Configuration Data Parsing: " + System.Environment.NewLine;

                            // Entry Number
                            txtField += iccRecDt.Rows[0][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][3].ToString().Trim() + System.Environment.NewLine;

                            // primaryAIDLength
                            txtField += iccRecDt.Rows[1][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][4].ToString().Trim() + System.Environment.NewLine;

                            // primaryAIDvalue
                            txtField += iccRecDt.Rows[2][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][5].ToString().Trim() + System.Environment.NewLine;

                            //  defaultAppLabelLength;
                            txtField += iccRecDt.Rows[3][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][6].ToString().Trim() + System.Environment.NewLine;

                            //  defaultAppLabelvalue
                            txtField += iccRecDt.Rows[4][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][7].ToString().Trim() + System.Environment.NewLine;

                            //primaryAIDICCAppType;
                            txtField += iccRecDt.Rows[5][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][8].ToString().Trim() + System.Environment.NewLine;

                            //private string primaryAIDLowestAppVersion;
                            txtField += iccRecDt.Rows[6][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][9].ToString().Trim() + System.Environment.NewLine;

                            //private string primaryAIDHighestAppVersion;
                            txtField += iccRecDt.Rows[7][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][10].ToString().Trim() + System.Environment.NewLine;

                            //private string primaryAIDActionCode;
                            txtField += iccRecDt.Rows[8][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][11].ToString().Trim() + System.Environment.NewLine;

                            //private string numberOfDataObjectTReq;
                            txtField += iccRecDt.Rows[9][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][12].ToString().Trim() + System.Environment.NewLine;

                            //private string dataObjectForTReq;
                            txtField += iccRecDt.Rows[10][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][13].ToString().Trim() + System.Environment.NewLine;

                            //private string numberOfDataObjectCompletion;
                            txtField += iccRecDt.Rows[11][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][14].ToString().Trim() + System.Environment.NewLine;

                            //private string dataObjectForCompletion;
                            txtField += iccRecDt.Rows[12][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][15].ToString().Trim() + System.Environment.NewLine;

                            //private string numberOfSecondaryAID;
                            txtField += iccRecDt.Rows[13][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][16].ToString().Trim() + System.Environment.NewLine;

                            //private string secondaryAIDLgthValue;
                            txtField += iccRecDt.Rows[14][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][17].ToString().Trim() + System.Environment.NewLine;

                            //private string appSelectionIndicator;
                            txtField += iccRecDt.Rows[16][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][18].ToString().Trim() + System.Environment.NewLine;

                            //private string trk2DataForCentral;
                            txtField += iccRecDt.Rows[17][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][19].ToString().Trim() + System.Environment.NewLine;

                            //private string trk2DataUsedDuringICCTransaction;
                            txtField += iccRecDt.Rows[18][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][20].ToString().Trim() + System.Environment.NewLine;

                            //private string additionalTrk2DataLength;
                            txtField += iccRecDt.Rows[19][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][21].ToString().Trim() + System.Environment.NewLine;

                            //private string additionalTrk2Data;
                            txtField += iccRecDt.Rows[20][3].ToString().Trim() + " = ";
                            txtField += dt.Rows[rowNum][22].ToString().Trim() + System.Environment.NewLine;

                    }
                }
            }

            return txtField;
        }
    }

}

