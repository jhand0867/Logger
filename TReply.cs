using System.Collections.Generic;
using System.Data;
using System.Net;

namespace Logger
{
    struct tReplyPrinterData
    {
        public string printerFlag;
        public string printerData;
    }

    struct tReplyCheckProcessing
    {
        private string checkID;
        private string checkDest;
        private string checkStamp;
        private string reserved4;
        private string checkEndorseText;

        public string CheckID
        {
            get { return checkID; }   // get method
            set { checkID = value; }  // set method
        }
        public string CheckDestination
        {
            get { return checkDest; }   // get method
            set { checkDest = value; }  // set method
        }
        public string CheckStamp
        {
            get { return checkStamp; }   // get method
            set { checkStamp = value; }  // set method
        }
        public string Reserved4
        {
            get { return reserved4; }   // get method
            set { reserved4 = value; }  // set method
        }
        public string CheckEndorseText
        {
            get { return checkEndorseText; }   // get method
            set { checkEndorseText = value; }  // set method
        }


    }
    struct transactionReply
    {
        private string messageClass;
        private string responseFlag;
        public string luno;
        public string timeVariant;
        public string nextState;
        public string numberOfType1Notes;
        public string numberOfType2Notes;
        public string numberOfType3Notes;
        public string numberOfType4Notes;
        public string numberOfType5Notes;
        public string numberOfType6Notes;
        public string numberOfType7Notes;
        public string numberofHopperType1Coins;
        public string numberofHopperType2Coins;
        public string numberofHopperType3Coins;
        public string numberofHopperType4Coins;
        public string numberofHopperType5Coins;
        public string serialNumber;
        public string functionID;
        public string screenNum;
        public string scrDisplayUpdate;
        public string messageCoordinatorNumber;
        public string cardReturnRetainFlag;
        public bool printerData;
        public string bufferID4;
        public string track3Data;
        public string bufferIDK;
        public string track1Data;
        public string bufferIDL;
        public string track2Data;
        public string VCDataIDM;
        public string VCDataM;
        public string VCDataIDN;
        public string VCDataN;
        public string VCDataIDO;
        public string VCDataO;
        public string VCDataIDP;
        public string VCDataP;
        public string VCDataIDQ;
        public string VCDataQ;
        public string VCDataIDR;
        public string VCDataR;
        public string bufferIDS;
        public string cashHandlerNumber;
        public string CassetteType1;
        public string numberOfBilsFromType1;
        public string CassetteType2;
        public string numberOfBilsFromType2;
        public string CassetteType3;
        public string numberOfBilsFromType3;
        public string CassetteType4;
        public string numberOfBilsFromType4;
        public string CassetteType5;
        public string numberOfBilsFromType5;
        public string CassetteType6;
        public string numberOfBilsFromType6;
        public string smartcardDataID5;
        public string smartcardData;
        public string checkDestDataIDa;
        public string checkDestData;
        public string processMultipleChecksIDb;
        public bool checksToProcess;
        public string useForEMVDCCTransactions;
        public string MACData;
        public int checksToProcessOffset;
    };

    class TReply : App, IMessage
    {
        public DataTable getDescription()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'R' ";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            return dt;

        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();

            string sql = @"SELECT TOP 1 * from treply WHERE logID = '" + logID + "' AND prjkey = '" + projectKey + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            sql = @"SELECT * from treplyPrinterData WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            sql = @"SELECT * from treplyCheckProcessing WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;
        }

        // get 4's from database

        public bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {

            foreach (typeRec r in typeRecs)
            {
                string[] tmpTypes = r.typeContent.Split((char)0x1c);

                transactionReply treply = parseTReply(r.typeContent);

                string printerData = tmpTypes[6].Substring(2, tmpTypes[6].Length - 2);
                List<tReplyPrinterData> rpdlist = parsePrinterData(printerData);
                if (rpdlist.Count > 0)
                    treply.printerData = true;

                List<tReplyCheckProcessing> chplist = new List<tReplyCheckProcessing>();
                if (treply.checksToProcessOffset > 0)
                {
                    string checksToProcess = tmpTypes[treply.checksToProcessOffset]
                            .Substring(1, tmpTypes[treply.checksToProcessOffset].Length - 1);
                    chplist = parseCheckProcessing(checksToProcess);
                    if (chplist.Count > 0)
                        treply.checksToProcess = true;
                }

                string sql = @"INSERT INTO treply([logkey],[rectype],[luno],[timeVariant],[nextState],[numberOfType1Notes]," +
                                "[numberOfType2Notes],[numberOfType3Notes],[numberOfType4Notes],[numberOfType5Notes],[numberOfType6Notes],[numberOfType7Notes]," +
                                "[numberofHopperType1Coins],[numberofHopperType2Coins],[numberofHopperType3Coins],[numberofHopperType4Coins],[numberofHopperType5Coins]," +
                                "[serialNumber],[functionID],[screenNum],[scrDisplayUpdate],[messageCoordinatorNumber],[cardReturnRetainFlag],[printerData]," +
                                "[bufferID4],[track3Data],[bufferIDK],[track1Data],[bufferIDL],[track2Data],[VCDataIDM],[VCDataM],[VCDataIDN],[VCDataN]," +
                                "[VCDataIDO],[VCDataO],[VCDataIDP],[VCDataP],[VCDataIDQ],[VCDataQ],[VCDataIDR],[VCDataR],[bufferIDS],[cashHandlerNumber]," +
                                "[CassetteType1],[numberOfBilsFromType1],[CassetteType2],[numberOfBilsFromType2],[CassetteType3]," +
                                "[numberOfBilsFromType3],[CassetteType4],[numberOfBilsFromType4],[CassetteType5],[numberOfBilsFromType5]," +
                                "[CassetteType6],[numberOfBilsFromType6],[smartcardDataID5],[smartcardData],[checkDestDataIDa],[checkDestData]," +
                                "[processMultipleChecksIDb],[checksToProcess],[useForEMVDCCTransactions],[MACData],[prjkey],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" +
                                   'R' + "','" +
                                   treply.luno + "','" +
                                   treply.timeVariant + "','" +
                                   treply.nextState + "','" +
                                   treply.numberOfType1Notes + "','" +
                                   treply.numberOfType2Notes + "','" +
                                   treply.numberOfType3Notes + "','" +
                                   treply.numberOfType4Notes + "','" +
                                   treply.numberOfType5Notes + "','" +
                                   treply.numberOfType6Notes + "','" +
                                   treply.numberOfType7Notes + "','" +
                                   treply.numberofHopperType1Coins + "','" +
                                   treply.numberofHopperType2Coins + "','" +
                                   treply.numberofHopperType3Coins + "','" +
                                   treply.numberofHopperType4Coins + "','" +
                                   treply.numberofHopperType5Coins + "','" +
                                   treply.serialNumber + "','" +
                                   treply.functionID + "','" +
                                   treply.screenNum + "','" +
                                   treply.scrDisplayUpdate + "','" +
                                   treply.messageCoordinatorNumber + "','" +
                                   treply.cardReturnRetainFlag + "','" +
                                   treply.printerData + "','" +
                                   treply.bufferID4 + "','" +
                                   treply.track3Data + "','" +
                                   treply.bufferIDK + "','" +
                                   treply.track1Data + "','" +
                                   treply.bufferIDL + "','" +
                                   treply.track2Data + "','" +
                                   treply.VCDataIDM + "','" +
                                   treply.VCDataM + "','" +
                                   treply.VCDataIDN + "','" +
                                   treply.VCDataN + "','" +
                                   treply.VCDataIDO + "','" +
                                   treply.VCDataO + "','" +
                                   treply.VCDataIDP + "','" +
                                   treply.VCDataP + "','" +
                                   treply.VCDataIDQ + "','" +
                                   treply.VCDataQ + "','" +
                                   treply.VCDataIDR + "','" +
                                   treply.VCDataR + "','" +
                                   treply.bufferIDS + "','" +
                                   treply.cashHandlerNumber + "','" +
                                   treply.CassetteType1 + "','" +
                                   treply.numberOfBilsFromType1 + "','" +
                                   treply.CassetteType2 + "','" +
                                   treply.numberOfBilsFromType2 + "','" +
                                   treply.CassetteType3 + "','" +
                                   treply.numberOfBilsFromType3 + "','" +
                                   treply.CassetteType4 + "','" +
                                   treply.numberOfBilsFromType4 + "','" +
                                   treply.CassetteType5 + "','" +
                                   treply.numberOfBilsFromType5 + "','" +
                                   treply.CassetteType6 + "','" +
                                   treply.numberOfBilsFromType6 + "','" +
                                   treply.smartcardDataID5 + "','" +
                                   treply.smartcardData + "','" +
                                   treply.checkDestDataIDa + "','" +
                                   treply.checkDestData + "','" +
                                   treply.processMultipleChecksIDb + "','" +
                                   treply.checksToProcess + "','" +
                                   treply.useForEMVDCCTransactions + "','" +
                                   treply.MACData + "','" +
                                   Key + "'," + logID + ")";

                DbCrud db = new DbCrud();

                if (db.crudToDb(sql) == false)
                    return false;

                // write Printer Data 

                foreach (tReplyPrinterData c in rpdlist)
                {

                    sql = @"INSERT INTO treplyPrinterData([logkey],[printerFlag],[printerData],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" +
                                   c.printerFlag + "','" +
                                   WebUtility.HtmlEncode(c.printerData) + "'," +
                                   logID + ")";
                    if (db.crudToDb(sql) == false)
                        return false;
                }

                // write Check Processing Data 

                foreach (tReplyCheckProcessing c in chplist)
                {
                    sql = @"INSERT INTO treplyPrinterData([logkey],[checkID],[checkDest],[checkStamp],[reserved4],[checkEndorseText],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" +
                                   c.CheckID + "','" +
                                   c.CheckDestination + "','" +
                                   c.CheckStamp + "','" +
                                   c.Reserved4 + "','" +
                                   c.CheckEndorseText + "'," +
                                   logID + ")";

                    if (db.crudToDb(sql) == false)
                        return false;
                }
            }
            return true;
        }

        private transactionReply parseTReply(string typeContent)
        {
            string[] tmpTypes = typeContent.Split((char)0x1c);

            transactionReply treply = new transactionReply();
            treply.luno = tmpTypes[1];
            treply.timeVariant = tmpTypes[2];
            treply.nextState = tmpTypes[3];

            if (tmpTypes[4].Length > 0)
            {
                char groupSeparator = (char)0x1d;
                string[] notesAndCoin = tmpTypes[4].Split(groupSeparator);

                int notes = notesAndCoin[0].Length;
                int offset = 0;

                treply.numberOfType1Notes = notesAndCoin[0].Substring(offset, 2);
                offset += 2;

                if (notes != offset)
                {
                    treply.numberOfType2Notes = notesAndCoin[0].Substring(offset, 2);
                    offset += 2;
                }
                if (notes != offset)
                {
                    treply.numberOfType3Notes = notesAndCoin[0].Substring(offset, 2);
                    offset += 2;
                }
                if (notes != offset)
                {
                    treply.numberOfType4Notes = notesAndCoin[0].Substring(offset, 2);
                    offset += 2;
                }
                if (notes != offset)
                {
                    treply.numberOfType5Notes = notesAndCoin[0].Substring(offset, 2);
                    offset += 2;
                }
                if (notes != offset)
                {
                    treply.numberOfType6Notes = notesAndCoin[0].Substring(offset, 2);
                    offset += 2;
                }
                if (notes != offset)
                {
                    treply.numberOfType7Notes = notesAndCoin[0].Substring(offset, 2);
                    offset += 2;
                }

                int coins = 0;
                offset = 0;

                if (notesAndCoin.Length > 1)
                {
                    coins = notesAndCoin[1].Length;
                }

                if (coins != offset)
                {
                    treply.numberofHopperType1Coins = notesAndCoin[1].Substring(offset, 2);
                    offset += 2;
                }

                if (coins != offset)
                {
                    treply.numberofHopperType2Coins = notesAndCoin[1].Substring(offset, 2);
                    offset += 2;
                }

                if (coins != offset)
                {
                    treply.numberofHopperType3Coins = notesAndCoin[1].Substring(offset, 2);
                    offset += 2;
                }

                if (coins != offset)
                {
                    treply.numberofHopperType4Coins = notesAndCoin[1].Substring(offset, 2);
                    offset += 2;
                }

                if (coins != offset)
                {
                    treply.numberofHopperType5Coins = notesAndCoin[1].Substring(offset, 2);
                    offset += 2;
                }
            }

            treply.serialNumber = tmpTypes[5].Substring(0, 4);
            treply.functionID = tmpTypes[5].Substring(4, 1);

            int i = 3;

            if ((tmpTypes[5].Substring(5, 1) == "u") ||
                (tmpTypes[5].Substring(5, 1) == "l"))
            {
                i = 5;
            }

            treply.screenNum = tmpTypes[5].Substring(5, i);
            i = i + 5;

            if (tmpTypes[5].Length > i)
            {
                treply.scrDisplayUpdate = tmpTypes[5].Substring(i, tmpTypes[5].Length - i);
            }

            treply.messageCoordinatorNumber = tmpTypes[6].Substring(0, 1);
            treply.cardReturnRetainFlag = tmpTypes[6].Substring(1, 1);

            treply.printerData = false;

            i = 7;

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "4")
            {
                treply.bufferID4 = tmpTypes[i].Substring(0, 1);
                treply.track3Data = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "K")
            {
                treply.bufferIDK = tmpTypes[i].Substring(0, 1);
                treply.track1Data = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "L")
            {
                treply.bufferIDL = tmpTypes[i].Substring(0, 1);
                treply.track2Data = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "M")
            {
                treply.VCDataIDM = tmpTypes[i].Substring(0, 1);
                treply.VCDataM = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "N")
            {
                treply.VCDataIDN = tmpTypes[i].Substring(0, 1);
                treply.VCDataN = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }


            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "O")
            {
                treply.VCDataIDO = tmpTypes[i].Substring(0, 1);
                treply.VCDataO = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "P")
            {
                treply.VCDataIDP = tmpTypes[i].Substring(0, 1);
                treply.VCDataP = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "Q")
            {
                treply.VCDataIDQ = tmpTypes[i].Substring(0, 1);
                treply.VCDataQ = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "R")
            {
                treply.VCDataIDR = tmpTypes[i].Substring(0, 1);
                treply.VCDataR = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "S")
            {
                treply.bufferIDS = tmpTypes[i].Substring(0, 1);
                int offset = 1;
                int bufferS = tmpTypes[i].Length;

                if (bufferS != offset)
                {
                    treply.cashHandlerNumber = tmpTypes[i].Substring(offset, 1);
                    offset = offset + 1;
                }
                if (bufferS != offset)
                {
                    treply.CassetteType1 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.numberOfBilsFromType1 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.CassetteType2 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.numberOfBilsFromType2 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.CassetteType3 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.numberOfBilsFromType3 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.CassetteType4 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.numberOfBilsFromType4 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.CassetteType5 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.numberOfBilsFromType5 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.CassetteType6 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.numberOfBilsFromType6 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                i++;
            }


            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "5")
            {
                if (tmpTypes[i].Substring(0, 4) == "5CAM")
                {
                    treply.smartcardDataID5 = tmpTypes[i].Substring(0, 1);
                    treply.smartcardData = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                    i++;
                }
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "a")
            {
                treply.checkDestDataIDa = tmpTypes[i].Substring(0, 1);
                treply.checkDestData = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }
            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }


            treply.checksToProcess = false;
            treply.checksToProcessOffset = 0;

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "b")
            {
                treply.processMultipleChecksIDb = tmpTypes[i].Substring(0, 1);

                // what do I have in buffer?
                treply.checksToProcessOffset = i;
                i++;
            }
            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Length == 1)
            {
                treply.useForEMVDCCTransactions = tmpTypes[i].Substring(0, 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0)
            {
                treply.MACData = tmpTypes[i];
                i++;
            }
            return treply;
        }

        private List<tReplyCheckProcessing> parseCheckProcessing(string checksToProcess)
        {
            List<tReplyCheckProcessing> chplist = new List<tReplyCheckProcessing>();

            string[] checksToProcessSplit = checksToProcess.Split((char)0x1d);
            foreach (string item in checksToProcessSplit)
            {
                tReplyCheckProcessing chp = new tReplyCheckProcessing();

                chp.CheckID = item.Substring(0, 3);
                chp.CheckDestination = item.Substring(3, 2);
                chp.CheckStamp = item.Substring(5, 1);
                chp.Reserved4 = item.Substring(6, 4);
                chp.CheckEndorseText = item.Substring(10, item.Length - 10);

                chplist.Add(chp);

            }

            return chplist;

        }

        private List<tReplyPrinterData> parsePrinterData(string printerData)
        {
            List<tReplyPrinterData> rpdlist = new List<tReplyPrinterData>();

            string[] printerDataSplit = printerData.Split((char)0x1d);
            foreach (string item in printerDataSplit)
            {
                tReplyPrinterData rpd = new tReplyPrinterData();

                rpd.printerFlag = item.Substring(0, 1);

                if (item.Length > 1)
                {
                    rpd.printerData = item.Substring(1, item.Length - 1);
                }

                rpdlist.Add(rpd);
            }

            return rpdlist;

        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = new List<DataTable>();
            dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable tReplyDt = getDescription();

            foreach (DataTable dt in dts)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                    {
                        for (int field = 3; field <= dt.Rows[rowNum].ItemArray.Length - 3; field++)
                        {
                            if (field == 24)
                            {
                                if (dts[1].Rows.Count > 0)
                                {
                                    txtField += getPrinterData(dts[1]);
                                }
                                continue;
                            }
                            if (field == 62)
                            {
                                if (dts[2].Rows.Count > 0)
                                {
                                    txtField += getCheckProccessing(dts[2]);
                                }
                                continue;
                            }
                            string fieldContent = dt.Rows[rowNum].ItemArray[field].ToString().Trim();
                            if (fieldContent != "")
                            { 
                                txtField += getOptionDescription(tReplyDt, field.ToString("00"), fieldContent);
                            }
                        }
                    }
                }
                break;
            }
            return txtField;
        }

        // mlh NOT TESTED

        private string getCheckProccessing(DataTable dt)
        {
            string checksData = "";
            if (dt.Rows.Count > 0)
            {
                for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                {
                    for (int fieldNum = 2; fieldNum < dt.Columns.Count - 1; fieldNum++)
                    {
                        checksData += dt.Columns[fieldNum].ColumnName.Trim() + "\t = " + dt.Rows[rowNum][fieldNum].ToString() + System.Environment.NewLine;
                    }
                }
            }
            return checksData;
        }

        private string getPrinterData(DataTable dataTable)
        {
            string printerData = "";

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    printerData += "Printer Flag\t = " + row["printerFlag"].ToString() + System.Environment.NewLine;
                    printerData += "Printer Data\t = " + WebUtility.HtmlDecode(row["printerData"].ToString()) + System.Environment.NewLine;
                }
            }
            return printerData;
        }
    }
}
