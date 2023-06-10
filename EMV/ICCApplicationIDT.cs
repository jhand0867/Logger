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
            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.LblTitle = "ICC Application IDT";
            lpb.Maximum = typeRecs.Count + 1;

            foreach (typeRec r in typeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

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
            lpb.Visible = false;
            return true;
        }
        public new List<iccApplication> parseData(string tmpTypes)
        {
            iccApplication iccApp = new iccApplication();
            List<iccApplication> iccAppList = new List<iccApplication>();
            Digester digester = LoggerFactory.Create_Digester();

            string[] tmpAids = tmpTypes.Split((char)0x1d);
            int hexLength = 0;


            for (int x = 0; x < tmpAids.Length; x++)
            {
                if (tmpAids[x].Length <= 2) continue;

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
                // ,
                hexLength = Convert.ToInt32(iccApp.NumberOfDataObjectTReq, 16);
                iccApp.DataObjectForTReq = digester.iccTLVTags(tmpAids[x].Substring(offset, tmpAids[x].Length - offset), hexLength);
                offset += iccApp.DataObjectForTReq.Length - hexLength;


                iccApp.NumberOfDataObjectCompletion = tmpAids[x].Substring(offset, 2);
                offset += 2;
                // ,
                hexLength = Convert.ToInt32(iccApp.NumberOfDataObjectCompletion, 16);

                if (hexLength > 0)
                {
                    iccApp.DataObjectForCompletion = digester.iccTLVTags(tmpAids[x].Substring(offset, tmpAids[x].Length - offset), hexLength);
                    string[] spacesArray = iccApp.DataObjectForCompletion.Split(' ');
                    // offset += iccApp.DataObjectForCompletion.Length - hexLength;
                    offset += iccApp.DataObjectForCompletion.Length - (spacesArray.Length - 1);
                }

                iccApp.NumberOfSecondaryAID = "";
                iccApp.SecondaryAIDLgthValue = "";
                hexLength = 0;

                if (offset < tmpAids[x].Length)
                {
                    iccApp.NumberOfSecondaryAID = tmpAids[x].Substring(offset, 2);
                    offset += 2;
                    hexLength = Convert.ToInt32(iccApp.NumberOfSecondaryAID, 16);
                }

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


        public override string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = new List<DataTable>();
            dts = getRecord(logKey, logID, projectKey);
            string txtField = "";
            string[] descriptionFields = new string[] { "", "", "" };

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            txtField = base.parseToView(logKey, logID, projectKey, recValue);

            DataTable iccRecDt = this.getDescription();

            if (dts[0].Rows.Count > 0)
                for (int rowNum = 0; rowNum < dts[0].Rows.Count; rowNum++)
                {
                    // Configuration Data

                    descriptionFields[0] = "Configuration Data Parsing: ";
                    descriptionFields[1] = "";
                    descriptionFields[2] = "";

                    txtField += App.Prj.insertRowRtf(descriptionFields);
                    for (int colNum = 3; colNum < dts[0].Columns.Count - 1; colNum++)
                        txtField += App.Prj.getOptionDescription(iccRecDt, "5" + colNum.ToString("00"), dts[0].Rows[rowNum][colNum].ToString());
                }

            return txtField;
        }
    }

}

