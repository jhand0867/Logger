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
        private string luno;
        private string timeVariant;
        private string nextState;
        private string numberOfType1Notes;
        private string numberOfType2Notes;
        private string numberOfType3Notes;
        private string numberOfType4Notes;
        private string numberOfType5Notes;
        private string numberOfType6Notes;
        private string numberOfType7Notes;
        private string numberofHopperType1Coins;
        private string numberofHopperType2Coins;
        private string numberofHopperType3Coins;
        private string numberofHopperType4Coins;
        private string numberofHopperType5Coins;
        private string serialNumber;
        private string functionID;
        private string screenNum;
        private string scrDisplayUpdate;
        private string messageCoordinatorNumber;
        private string cardReturnRetainFlag;
        private bool printerData;
        private string bufferID4;
        private string track3Data;
        private string bufferIDK;
        private string track1Data;
        private string bufferIDL;
        private string track2Data;
        private string VCDataIDM;
        private string VCDataM;
        private string VCDataIDN;
        private string VCDataN;
        private string VCDataIDO;
        private string VCDataO;
        private string VCDataIDP;
        private string VCDataP;
        private string VCDataIDQ;
        private string VCDataQ;
        private string VCDataIDR;
        private string VCDataR;
        private string bufferIDS;
        private string cashHandlerNumber;
        private string CassetteType1;
        private string numberOfBilsFromType1;
        private string CassetteType2;
        private string numberOfBilsFromType2;
        private string CassetteType3;
        private string numberOfBilsFromType3;
        private string CassetteType4;
        private string numberOfBilsFromType4;
        private string CassetteType5;
        private string numberOfBilsFromType5;
        private string CassetteType6;
        private string numberOfBilsFromType6;
        private string smartcardDataID5;
        private string smartcardData;
        private string checkDestDataIDa;
        private string checkDestData;
        private string processMultipleChecksIDb;
        private bool checksToProcess;
        private string useForEMVDCCTransactions;
        private string MACData;
        private int checksToProcessOffset;

        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string ResponseFlag { get => responseFlag; set => responseFlag = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string NextState { get => nextState; set => nextState = value; }
        public string NumberOfType1Notes { get => numberOfType1Notes; set => numberOfType1Notes = value; }
        public string NumberOfType2Notes { get => numberOfType2Notes; set => numberOfType2Notes = value; }
        public string NumberOfType3Notes { get => numberOfType3Notes; set => numberOfType3Notes = value; }
        public string NumberOfType4Notes { get => numberOfType4Notes; set => numberOfType4Notes = value; }
        public string NumberOfType5Notes { get => numberOfType5Notes; set => numberOfType5Notes = value; }
        public string NumberOfType6Notes { get => numberOfType6Notes; set => numberOfType6Notes = value; }
        public string NumberOfType7Notes { get => numberOfType7Notes; set => numberOfType7Notes = value; }
        public string NumberofHopperType1Coins { get => numberofHopperType1Coins; set => numberofHopperType1Coins = value; }
        public string NumberofHopperType2Coins { get => numberofHopperType2Coins; set => numberofHopperType2Coins = value; }
        public string NumberofHopperType3Coins { get => numberofHopperType3Coins; set => numberofHopperType3Coins = value; }
        public string NumberofHopperType4Coins { get => numberofHopperType4Coins; set => numberofHopperType4Coins = value; }
        public string NumberofHopperType5Coins { get => numberofHopperType5Coins; set => numberofHopperType5Coins = value; }
        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public string FunctionID { get => functionID; set => functionID = value; }
        public string ScreenNum { get => screenNum; set => screenNum = value; }
        public string ScrDisplayUpdate { get => scrDisplayUpdate; set => scrDisplayUpdate = value; }
        public string MessageCoordinatorNumber { get => messageCoordinatorNumber; set => messageCoordinatorNumber = value; }
        public string CardReturnRetainFlag { get => cardReturnRetainFlag; set => cardReturnRetainFlag = value; }
        public bool PrinterData { get => printerData; set => printerData = value; }
        public string BufferID4 { get => bufferID4; set => bufferID4 = value; }
        public string Track3Data { get => track3Data; set => track3Data = value; }
        public string BufferIDK { get => bufferIDK; set => bufferIDK = value; }
        public string Track1Data { get => track1Data; set => track1Data = value; }
        public string BufferIDL { get => bufferIDL; set => bufferIDL = value; }
        public string Track2Data { get => track2Data; set => track2Data = value; }
        public string VCDataIDM1 { get => VCDataIDM; set => VCDataIDM = value; }
        public string VCDataM1 { get => VCDataM; set => VCDataM = value; }
        public string VCDataIDN1 { get => VCDataIDN; set => VCDataIDN = value; }
        public string VCDataN1 { get => VCDataN; set => VCDataN = value; }
        public string VCDataIDO1 { get => VCDataIDO; set => VCDataIDO = value; }
        public string VCDataO1 { get => VCDataO; set => VCDataO = value; }
        public string VCDataIDP1 { get => VCDataIDP; set => VCDataIDP = value; }
        public string VCDataP1 { get => VCDataP; set => VCDataP = value; }
        public string VCDataIDQ1 { get => VCDataIDQ; set => VCDataIDQ = value; }
        public string VCDataQ1 { get => VCDataQ; set => VCDataQ = value; }
        public string VCDataIDR1 { get => VCDataIDR; set => VCDataIDR = value; }
        public string VCDataR1 { get => VCDataR; set => VCDataR = value; }
        public string BufferIDS { get => bufferIDS; set => bufferIDS = value; }
        public string CashHandlerNumber { get => cashHandlerNumber; set => cashHandlerNumber = value; }
        public string CassetteType11 { get => CassetteType1; set => CassetteType1 = value; }
        public string NumberOfBilsFromType1 { get => numberOfBilsFromType1; set => numberOfBilsFromType1 = value; }
        public string CassetteType21 { get => CassetteType2; set => CassetteType2 = value; }
        public string NumberOfBilsFromType2 { get => numberOfBilsFromType2; set => numberOfBilsFromType2 = value; }
        public string CassetteType31 { get => CassetteType3; set => CassetteType3 = value; }
        public string NumberOfBilsFromType3 { get => numberOfBilsFromType3; set => numberOfBilsFromType3 = value; }
        public string CassetteType41 { get => CassetteType4; set => CassetteType4 = value; }
        public string NumberOfBilsFromType4 { get => numberOfBilsFromType4; set => numberOfBilsFromType4 = value; }
        public string CassetteType51 { get => CassetteType5; set => CassetteType5 = value; }
        public string NumberOfBilsFromType5 { get => numberOfBilsFromType5; set => numberOfBilsFromType5 = value; }
        public string CassetteType61 { get => CassetteType6; set => CassetteType6 = value; }
        public string NumberOfBilsFromType6 { get => numberOfBilsFromType6; set => numberOfBilsFromType6 = value; }
        public string SmartcardDataID5 { get => smartcardDataID5; set => smartcardDataID5 = value; }
        public string SmartcardData { get => smartcardData; set => smartcardData = value; }
        public string CheckDestDataIDa { get => checkDestDataIDa; set => checkDestDataIDa = value; }
        public string CheckDestData { get => checkDestData; set => checkDestData = value; }
        public string ProcessMultipleChecksIDb { get => processMultipleChecksIDb; set => processMultipleChecksIDb = value; }
        public bool ChecksToProcess { get => checksToProcess; set => checksToProcess = value; }
        public string UseForEMVDCCTransactions { get => useForEMVDCCTransactions; set => useForEMVDCCTransactions = value; }
        public string MACData1 { get => MACData; set => MACData = value; }
        public int ChecksToProcessOffset { get => checksToProcessOffset; set => checksToProcessOffset = value; }
    }
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

            string sql = @"SELECT * from treply WHERE logID = '" + logID + "' AND prjkey = '" + projectKey + "' AND logkey LIKE '" + logKey + "%' LIMIT 1";
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
            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.Maximum = typeRecs.Count + 1;
            lpb.LblTitle = this.ToString();

            foreach (typeRec r in typeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                string[] tmpTypes = r.typeContent.Split((char)0x1c);

                transactionReply treply = parseTReply(r.typeContent);

                string printerData = tmpTypes[6].Substring(2, tmpTypes[6].Length - 2);
                List<tReplyPrinterData> rpdlist = parsePrinterData(printerData);
                if (rpdlist.Count > 0)
                    treply.PrinterData = true;

                List<tReplyCheckProcessing> chplist = new List<tReplyCheckProcessing>();
                if (treply.ChecksToProcessOffset > 0)
                {
                    string checksToProcess = tmpTypes[treply.ChecksToProcessOffset]
                            .Substring(1, tmpTypes[treply.ChecksToProcessOffset].Length - 1);
                    chplist = parseCheckProcessing(checksToProcess);
                    if (chplist.Count > 0)
                        treply.ChecksToProcess = true;
                }

                string sql = @"INSERT INTO treply([logkey],[rectype],[messageClass],[responseFlag],[luno],[timeVariant],[nextState],[numberOfType1Notes]," +
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
                                   treply.MessageClass + "','" +
                                   treply.ResponseFlag + "','" +
                                   treply.Luno + "','" +
                                   treply.TimeVariant + "','" +
                                   treply.NextState + "','" +
                                   treply.NumberOfType1Notes + "','" +
                                   treply.NumberOfType2Notes + "','" +
                                   treply.NumberOfType3Notes + "','" +
                                   treply.NumberOfType4Notes + "','" +
                                   treply.NumberOfType5Notes + "','" +
                                   treply.NumberOfType6Notes + "','" +
                                   treply.NumberOfType7Notes + "','" +
                                   treply.NumberofHopperType1Coins + "','" +
                                   treply.NumberofHopperType2Coins + "','" +
                                   treply.NumberofHopperType3Coins + "','" +
                                   treply.NumberofHopperType4Coins + "','" +
                                   treply.NumberofHopperType5Coins + "','" +
                                   treply.SerialNumber + "','" +
                                   treply.FunctionID + "','" +
                                   treply.ScreenNum + "','" +
                                   treply.ScrDisplayUpdate + "','" +
                                   treply.MessageCoordinatorNumber + "','" +
                                   treply.CardReturnRetainFlag + "','" +
                                   treply.PrinterData + "','" +
                                   treply.BufferID4 + "','" +
                                   treply.Track3Data + "','" +
                                   treply.BufferIDK + "','" +
                                   treply.Track1Data + "','" +
                                   treply.BufferIDL + "','" +
                                   treply.Track2Data + "','" +
                                   treply.VCDataIDM1 + "','" +
                                   treply.VCDataM1 + "','" +
                                   treply.VCDataIDN1 + "','" +
                                   treply.VCDataN1 + "','" +
                                   treply.VCDataIDO1 + "','" +
                                   treply.VCDataO1 + "','" +
                                   treply.VCDataIDP1 + "','" +
                                   treply.VCDataP1 + "','" +
                                   treply.VCDataIDQ1 + "','" +
                                   treply.VCDataQ1 + "','" +
                                   treply.VCDataIDR1 + "','" +
                                   treply.VCDataR1 + "','" +
                                   treply.BufferIDS + "','" +
                                   treply.CashHandlerNumber + "','" +
                                   treply.CassetteType11 + "','" +
                                   treply.NumberOfBilsFromType1 + "','" +
                                   treply.CassetteType21 + "','" +
                                   treply.NumberOfBilsFromType2 + "','" +
                                   treply.CassetteType31 + "','" +
                                   treply.NumberOfBilsFromType3 + "','" +
                                   treply.CassetteType41 + "','" +
                                   treply.NumberOfBilsFromType4 + "','" +
                                   treply.CassetteType51 + "','" +
                                   treply.NumberOfBilsFromType5 + "','" +
                                   treply.CassetteType61 + "','" +
                                   treply.NumberOfBilsFromType6 + "','" +
                                   treply.SmartcardDataID5 + "','" +
                                   treply.SmartcardData + "','" +
                                   treply.CheckDestDataIDa + "','" +
                                   treply.CheckDestData + "','" +
                                   treply.ProcessMultipleChecksIDb + "','" +
                                   treply.ChecksToProcess + "','" +
                                   treply.UseForEMVDCCTransactions + "','" +
                                   treply.MACData1 + "','" +
                                   Key + "'," + logID + ")";

                DbCrud db = new DbCrud();

                if (db.crudToDb(sql) == false)
                {
                    return false;
                }

                // write Printer Data 

                foreach (tReplyPrinterData c in rpdlist)
                {

                    sql = @"INSERT INTO treplyPrinterData([logkey],[printerFlag],[printerData],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" +
                                   c.printerFlag + "','" +
                                   WebUtility.HtmlEncode(c.printerData) + "'," +
                                   logID + ")";
                    if (db.crudToDb(sql) == false)
                    {
                        return false;
                    }
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
                    {
                        return false;
                    }
                }
            }

            lpb.Visible = false;
            return true;
        }

        private transactionReply parseTReply(string typeContent)
        {
            string[] tmpTypes = typeContent.Split((char)0x1c);

            transactionReply treply = new transactionReply();

            treply.MessageClass = tmpTypes[0].Substring(10, 1);

            if (tmpTypes[0].Length > 11)
                treply.ResponseFlag = tmpTypes[0].Substring(11, 1);

            treply.Luno = tmpTypes[1];
            treply.TimeVariant = tmpTypes[2];
            treply.NextState = tmpTypes[3];

            if (tmpTypes[4].Length > 0)
            {
                char groupSeparator = (char)0x1d;
                string[] notesAndCoin = tmpTypes[4].Split(groupSeparator);

                int notes = notesAndCoin[0].Length;
                int offset = 0;

                treply.NumberOfType1Notes = notesAndCoin[0].Substring(offset, 2);
                offset += 2;

                if (notes != offset)
                {
                    treply.NumberOfType2Notes = notesAndCoin[0].Substring(offset, 2);
                    offset += 2;
                }
                if (notes != offset)
                {
                    treply.NumberOfType3Notes = notesAndCoin[0].Substring(offset, 2);
                    offset += 2;
                }
                if (notes != offset)
                {
                    treply.NumberOfType4Notes = notesAndCoin[0].Substring(offset, 2);
                    offset += 2;
                }
                if (notes != offset)
                {
                    treply.NumberOfType5Notes = notesAndCoin[0].Substring(offset, 2);
                    offset += 2;
                }
                if (notes != offset)
                {
                    treply.NumberOfType6Notes = notesAndCoin[0].Substring(offset, 2);
                    offset += 2;
                }
                if (notes != offset)
                {
                    treply.NumberOfType7Notes = notesAndCoin[0].Substring(offset, 2);
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
                    treply.NumberofHopperType1Coins = notesAndCoin[1].Substring(offset, 2);
                    offset += 2;
                }

                if (coins != offset)
                {
                    treply.NumberofHopperType2Coins = notesAndCoin[1].Substring(offset, 2);
                    offset += 2;
                }

                if (coins != offset)
                {
                    treply.NumberofHopperType3Coins = notesAndCoin[1].Substring(offset, 2);
                    offset += 2;
                }

                if (coins != offset)
                {
                    treply.NumberofHopperType4Coins = notesAndCoin[1].Substring(offset, 2);
                    offset += 2;
                }

                if (coins != offset)
                {
                    treply.NumberofHopperType5Coins = notesAndCoin[1].Substring(offset, 2);
                    offset += 2;
                }
            }

            treply.SerialNumber = tmpTypes[5].Substring(0, 4);
            treply.FunctionID = tmpTypes[5].Substring(4, 1);

            int i = 3;

            if ((tmpTypes[5].Substring(5, 1) == "u") ||
                (tmpTypes[5].Substring(5, 1) == "l"))
            {
                i = 5;
            }

            treply.ScreenNum = tmpTypes[5].Substring(5, i);
            i = i + 5;

            if (tmpTypes[5].Length > i)
            {
                treply.ScrDisplayUpdate = tmpTypes[5].Substring(i, tmpTypes[5].Length - i);
            }

            treply.MessageCoordinatorNumber = tmpTypes[6].Substring(0, 1);
            treply.CardReturnRetainFlag = tmpTypes[6].Substring(1, 1);

            treply.PrinterData = false;

            i = 7;

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "4")
            {
                treply.BufferID4 = tmpTypes[i].Substring(0, 1);
                treply.Track3Data = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "K")
            {
                treply.BufferIDK = tmpTypes[i].Substring(0, 1);
                treply.Track1Data = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "L")
            {
                treply.BufferIDL = tmpTypes[i].Substring(0, 1);
                treply.Track2Data = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "M")
            {
                treply.VCDataIDM1 = tmpTypes[i].Substring(0, 1);
                treply.VCDataM1 = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "N")
            {
                treply.VCDataIDN1 = tmpTypes[i].Substring(0, 1);
                treply.VCDataN1 = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }


            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "O")
            {
                treply.VCDataIDO1 = tmpTypes[i].Substring(0, 1);
                treply.VCDataO1 = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "P")
            {
                treply.VCDataIDP1 = tmpTypes[i].Substring(0, 1);
                treply.VCDataP1 = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "Q")
            {
                treply.VCDataIDQ1 = tmpTypes[i].Substring(0, 1);
                treply.VCDataQ1 = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "R")
            {
                treply.VCDataIDR1 = tmpTypes[i].Substring(0, 1);
                treply.VCDataR1 = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "S")
            {
                treply.BufferIDS = tmpTypes[i].Substring(0, 1);
                int offset = 1;
                int bufferS = tmpTypes[i].Length;

                if (bufferS != offset)
                {
                    treply.CashHandlerNumber = tmpTypes[i].Substring(offset, 1);
                    offset = offset + 1;
                }
                if (bufferS != offset)
                {
                    treply.CassetteType11 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.NumberOfBilsFromType1 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.CassetteType21 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.NumberOfBilsFromType2 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.CassetteType31 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.NumberOfBilsFromType3 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.CassetteType41 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.NumberOfBilsFromType4 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.CassetteType51 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.NumberOfBilsFromType5 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.CassetteType61 = tmpTypes[i].Substring(offset, 3);
                    offset = offset + 3;
                }
                if (bufferS != offset)
                {
                    treply.NumberOfBilsFromType6 = tmpTypes[i].Substring(offset, 3);
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
                    treply.SmartcardDataID5 = tmpTypes[i].Substring(0, 1);
                    treply.SmartcardData = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                    i++;
                }
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "a")
            {
                treply.CheckDestDataIDa = tmpTypes[i].Substring(0, 1);
                treply.CheckDestData = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                i++;
            }
            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }


            treply.ChecksToProcess = false;
            treply.ChecksToProcessOffset = 0;

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Substring(0, 1) == "b")
            {
                treply.ProcessMultipleChecksIDb = tmpTypes[i].Substring(0, 1);

                // what do I have in buffer?
                treply.ChecksToProcessOffset = i;
                i++;
            }
            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                tmpTypes[i].Length == 1)
            {
                treply.UseForEMVDCCTransactions = tmpTypes[i].Substring(0, 1);
                i++;
            }

            if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

            if (tmpTypes.Length > i && tmpTypes[i].Length > 0)
            {
                treply.MACData1 = tmpTypes[i];
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
                if (item.Length == 0) continue;

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
                            if (field == 26)
                            {
                                if (dts[1].Rows.Count > 0)
                                {
                                    txtField += getPrinterData(dts[1]);
                                }
                                continue;
                            }
                            if (field == 64)
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
                        checksData += @"\intbl " + dt.Columns[fieldNum].ColumnName.Trim() + " = " + @"\cell " + dt.Rows[rowNum][fieldNum].ToString() + @"\row ";
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
                    printerData += @"\intbl " + "Printer Flag = " + @"\cell " + row["printerFlag"].ToString() + @"\row ";
                    printerData += @"\intbl " + "Printer Data = " + @"\cell " + WebUtility.HtmlDecode(row["printerData"].ToString()) + @"\row ";
                }
            }
            return printerData;
        }
    }
}
