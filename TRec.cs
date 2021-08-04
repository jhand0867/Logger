using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    public struct lastTransactionStatus
    {
        private string serialNumber;
        private string lastStatusIssued;
        private string lastTransactionNotesDispenseData;
        private string lastTransactionCoinageAmountDispensed;
        private string lastTransactionCoinsDispenseData;
        private string lastTransactionCashDepositData;

        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public string LastStatusIssued { get => lastStatusIssued; set => lastStatusIssued = value; }
        public string LastTransactionNotesDispenseData { get => lastTransactionNotesDispenseData; set => lastTransactionNotesDispenseData = value; }
        public string LastTransactionCoinageAmountDispensed { get => lastTransactionCoinageAmountDispensed; set => lastTransactionCoinageAmountDispensed = value; }
        public string LastTransactionCoinsDispenseData { get => lastTransactionCoinsDispenseData; set => lastTransactionCoinsDispenseData = value; }
        public string LastTransactionCashDepositData { get => lastTransactionCashDepositData; set => lastTransactionCashDepositData = value; }
    }

    public struct depCurrency
    {
        private string depositCurrency;
        private string amountExponentSign;
        private string amountExponentValue;
        private string totalCustomerAmount;
        private string totalDeriveAmount;
        private string zero42;
        private int numOfChecks;

        public string DepositCurrency { get => depositCurrency; set => depositCurrency = value; }
        public string AmountExponentSign { get => amountExponentSign; set => amountExponentSign = value; }
        public string AmountExponentValue { get => amountExponentValue; set => amountExponentValue = value; }
        public string TotalCustomerAmount { get => totalCustomerAmount; set => totalCustomerAmount = value; }
        public string TotalDeriveAmount { get => totalDeriveAmount; set => totalDeriveAmount = value; }
        public string Zero42 { get => zero42; set => zero42 = value; }
        public int NumOfChecks { get => numOfChecks; set => numOfChecks = value; }
    }
    public struct checks
    {
        public string depositCurrency;
        public string checkId;
        public string customerCheckAmount;
        public string deriveCheckAmount;
        public string codelineLength;
        public string codelineData;
    };

    public struct transactionRequest
    {
        private string messageClass;
        private string messageSubclass;
        private string luno;
        private string timeVariantNumber;
        private string topOfReceiptFlag;
        private string messageCoordinationNumber;
        private string track2Data;
        private string track3Data;
        private string operationCodeData;
        private string amountEntryField;
        private string PINBuffer;
        private string BufferB;
        private string BufferC;
        private string track1Identier;
        private string track1Data;
        private string transactionStatusDataID;
        private lastTransactionStatus lastTransactionStatusData;
        private string CSPDataIdU;
        private string CSPDataU;
        private string confirmationCSPDataIdV;
        private string confirmationCSPDataV;
        private string vcDataIdW;
        private string vcDataW;
        private string vcDataIdX;
        private string vcDataX;
        private string vcDataIdY;
        private string vcDataY;
        private string vcDataIdZ;
        private string vcDataZ;
        private string vcDataIDBracket;
        private string vcDataBracket;
        private string vcDataIDSlash;
        private string vcDataSlash;
        private string smartcardID5;
        private string smartcardData;
        private string deviceIdw;
        private bool notesType;
        private string documentDataId;
        private string MICRDetected;
        private string MICRValue;
        private string fieldIdE;
        private string barcodeFormatId;
        private string reserved;
        private string scanBarcodeData;
        private string fieldIDf;
        private string numberOfCoinfromHopper1;
        private string numberOfCoinfromHopper2;
        private string numberOfCoinfromHopper3;
        private string numberOfCoinfromHopper4;
        private string numberOfCoinfromHopper5;
        private string numberOfCoinfromHopper6;
        private string numberOfCoinfromHopper7;
        private string numberOfCoinfromHopper8;
        private string dataIDg;
        private string totalChecksToReturn;
        private string zero41;
        private int numberOfCurrencies;
        private string fieldIdLessThan;
        private string VGLanguageId;
        private string optionalDataFields;
        private string optionalData;
        private string MAC;

        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubclass { get => messageSubclass; set => messageSubclass = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariantNumber { get => timeVariantNumber; set => timeVariantNumber = value; }
        public string TopOfReceiptFlag { get => topOfReceiptFlag; set => topOfReceiptFlag = value; }
        public string MessageCoordinationNumber { get => messageCoordinationNumber; set => messageCoordinationNumber = value; }
        public string Track2Data { get => track2Data; set => track2Data = value; }
        public string Track3Data { get => track3Data; set => track3Data = value; }
        public string OperationCodeData { get => operationCodeData; set => operationCodeData = value; }
        public string AmountEntryField { get => amountEntryField; set => amountEntryField = value; }
        public string PINBuffer1 { get => PINBuffer; set => PINBuffer = value; }
        public string BufferB1 { get => BufferB; set => BufferB = value; }
        public string BufferC1 { get => BufferC; set => BufferC = value; }
        public string Track1Identier { get => track1Identier; set => track1Identier = value; }
        public string Track1Data { get => track1Data; set => track1Data = value; }
        public string TransactionStatusDataID { get => transactionStatusDataID; set => transactionStatusDataID = value; }
        public lastTransactionStatus LastTransactionStatusData { get => lastTransactionStatusData; set => lastTransactionStatusData = value; }
        public string CSPDataIdU1 { get => CSPDataIdU; set => CSPDataIdU = value; }
        public string CSPDataU1 { get => CSPDataU; set => CSPDataU = value; }
        public string ConfirmationCSPDataIdV { get => confirmationCSPDataIdV; set => confirmationCSPDataIdV = value; }
        public string ConfirmationCSPDataV { get => confirmationCSPDataV; set => confirmationCSPDataV = value; }
        public string VcDataIdW { get => vcDataIdW; set => vcDataIdW = value; }
        public string VcDataW { get => vcDataW; set => vcDataW = value; }
        public string VcDataIdX { get => vcDataIdX; set => vcDataIdX = value; }
        public string VcDataX { get => vcDataX; set => vcDataX = value; }
        public string VcDataIdY { get => vcDataIdY; set => vcDataIdY = value; }
        public string VcDataY { get => vcDataY; set => vcDataY = value; }
        public string VcDataIdZ { get => vcDataIdZ; set => vcDataIdZ = value; }
        public string VcDataZ { get => vcDataZ; set => vcDataZ = value; }
        public string VcDataIDBracket { get => vcDataIDBracket; set => vcDataIDBracket = value; }
        public string VcDataBracket { get => vcDataBracket; set => vcDataBracket = value; }
        public string VcDataIDSlash { get => vcDataIDSlash; set => vcDataIDSlash = value; }
        public string VcDataSlash { get => vcDataSlash; set => vcDataSlash = value; }
        public string SmartcardID5 { get => smartcardID5; set => smartcardID5 = value; }
        public string SmartcardData { get => smartcardData; set => smartcardData = value; }
        public string DeviceIdw { get => deviceIdw; set => deviceIdw = value; }
        public bool NotesType { get => notesType; set => notesType = value; }
        public string DocumentDataId { get => documentDataId; set => documentDataId = value; }
        public string MICRDetected1 { get => MICRDetected; set => MICRDetected = value; }
        public string MICRValue1 { get => MICRValue; set => MICRValue = value; }
        public string FieldIdE { get => fieldIdE; set => fieldIdE = value; }
        public string BarcodeFormatId { get => barcodeFormatId; set => barcodeFormatId = value; }
        public string Reserved { get => reserved; set => reserved = value; }
        public string ScanBarcodeData { get => scanBarcodeData; set => scanBarcodeData = value; }
        public string FieldIDf { get => fieldIDf; set => fieldIDf = value; }
        public string NumberOfCoinfromHopper1 { get => numberOfCoinfromHopper1; set => numberOfCoinfromHopper1 = value; }
        public string NumberOfCoinfromHopper2 { get => numberOfCoinfromHopper2; set => numberOfCoinfromHopper2 = value; }
        public string NumberOfCoinfromHopper3 { get => numberOfCoinfromHopper3; set => numberOfCoinfromHopper3 = value; }
        public string NumberOfCoinfromHopper4 { get => numberOfCoinfromHopper4; set => numberOfCoinfromHopper4 = value; }
        public string NumberOfCoinfromHopper5 { get => numberOfCoinfromHopper5; set => numberOfCoinfromHopper5 = value; }
        public string NumberOfCoinfromHopper6 { get => numberOfCoinfromHopper6; set => numberOfCoinfromHopper6 = value; }
        public string NumberOfCoinfromHopper7 { get => numberOfCoinfromHopper7; set => numberOfCoinfromHopper7 = value; }
        public string NumberOfCoinfromHopper8 { get => numberOfCoinfromHopper8; set => numberOfCoinfromHopper8 = value; }
        public string DataIDg { get => dataIDg; set => dataIDg = value; }
        public string TotalChecksToReturn { get => totalChecksToReturn; set => totalChecksToReturn = value; }
        public string Zero41 { get => zero41; set => zero41 = value; }
        public int NumberOfCurrencies { get => numberOfCurrencies; set => numberOfCurrencies = value; }
        public string FieldIdLessThan { get => fieldIdLessThan; set => fieldIdLessThan = value; }
        public string VGLanguageId1 { get => VGLanguageId; set => VGLanguageId = value; }
        public string OptionalDataFields { get => optionalDataFields; set => optionalDataFields = value; }
        public string OptionalData { get => optionalData; set => optionalData = value; }
        public string MAC1 { get => MAC; set => MAC = value; }
    }
    public struct parameterAndValue
    {
        public string paramName;
        public string paramValue;
    };

    class TRec : App, IMessage
    {
        public DataTable getDescription()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'T' ";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            return dt;
        }

        public bool writeData(List<typeRec> typeRecs, string key, string logID)
        {
            string[] tmpTypes;
            String sql = "";
            int loadNum = 0;

            foreach (typeRec r in typeRecs)
            {
                tmpTypes = r.typeContent.Split((char)0x1c);

                transactionRequest treq = new transactionRequest();
                treq.Luno = tmpTypes[1];

                treq.TimeVariantNumber = tmpTypes[3];
                treq.TopOfReceiptFlag = tmpTypes[4].Substring(0, 1);
                treq.MessageCoordinationNumber = tmpTypes[4].Substring(1, 1);
                treq.Track2Data = tmpTypes[5];
                treq.Track3Data = tmpTypes[6];
                treq.OperationCodeData = tmpTypes[7];
                treq.AmountEntryField = tmpTypes[8];
                treq.PINBuffer1 = tmpTypes[9];
                treq.BufferB1 = tmpTypes[10];
                treq.BufferC1 = tmpTypes[11];

                int i = 12;

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                    tmpTypes[i].Substring(0, 1) == "1")
                {
                    treq.Track1Identier = tmpTypes[i].Substring(0, 1);
                    treq.Track1Data = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                    i++;
                }

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                    tmpTypes[i].Substring(0, 1) == "2")
                {
                    sql = @"SELECT [optionNum] FROM [enhancedParams] " +
                                  "WHERE [optionCode] ='76'";
                    int fourOrSeven = 20;
                    Dictionary<string, string> resultData = readData(sql);

                    treq.TransactionStatusDataID = tmpTypes[i].Substring(0, 1);
                    lastTransactionStatus lst = new lastTransactionStatus();
                    lst.SerialNumber  = tmpTypes[i].Substring(1, 4);
                    lst.LastStatusIssued = tmpTypes[i].Substring(5, 1);

                    if ((resultData.Count == 0) ||
                       (resultData.Count > 0 && resultData.ContainsValue("000")))
                    {
                        if (tmpTypes[i].Length > 25)
                        {
                            lst.LastTransactionNotesDispenseData =
                            tmpTypes[i].Substring(6, 20);
                        }
                        else
                        {
                            Console.WriteLine(" Error Treq.lastTransactionStatusData");
                        }
                    }
                    else
                    {
                        if (tmpTypes[i].Length > 40)
                        {
                            fourOrSeven = 35;
                            lst.LastTransactionNotesDispenseData =
                            tmpTypes[i].Substring(6, 35);
                        }
                        else
                        {
                            Console.WriteLine(" Error Treq.lastTransactionStatusData");
                        }
                    }

                    fourOrSeven = fourOrSeven + 6;

                    if (tmpTypes[i].Length > fourOrSeven + 25)
                    {
                        lst.LastTransactionCoinageAmountDispensed =
                        tmpTypes[i].Substring(fourOrSeven, 5);

                        fourOrSeven = fourOrSeven + 5;
                        lst.LastTransactionCoinsDispenseData =
                            tmpTypes[i].Substring(fourOrSeven, 20);

                        fourOrSeven = fourOrSeven + 20;
                        lst.LastTransactionCashDepositData =
                            tmpTypes[i].Substring(fourOrSeven, tmpTypes[i].Length - fourOrSeven);
                    }
                    treq.LastTransactionStatusData = lst;
                    i++;
                }

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                    tmpTypes[i].Substring(0, 1) == "U")
                {
                    treq.CSPDataIdU1 = tmpTypes[i].Substring(0, 1);
                    treq.CSPDataU1 = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                    i++;
                }

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "V")
                {
                    treq.ConfirmationCSPDataIdV = tmpTypes[i].Substring(0, 1);
                    treq.ConfirmationCSPDataV = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                    i++;
                }

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "W")
                {
                    treq.VcDataIdW = tmpTypes[i].Substring(0, 1);
                    treq.VcDataW = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                    i++;
                }

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "X")
                {
                    treq.VcDataIdX = tmpTypes[i].Substring(0, 1);
                    treq.VcDataX = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                    i++;
                }

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "Y")
                {
                    treq.VcDataIdY = tmpTypes[i].Substring(0, 1);
                    treq.VcDataY = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                    i++;
                }

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "Z")
                {
                    treq.VcDataIdZ = tmpTypes[i].Substring(0, 1);
                    treq.VcDataZ = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                    i++;
                }

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "[")
                {
                    treq.VcDataIDBracket = tmpTypes[i].Substring(0, 1);
                    treq.VcDataBracket = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                    i++;
                }

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == @"\")
                {
                    treq.VcDataIDSlash = tmpTypes[i].Substring(0, 1);
                    treq.VcDataSlash = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                    i++;
                }

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "5")
                {
                    if (tmpTypes[i].Substring(0, 4) == "5CAM")
                    {
                        treq.SmartcardID5 = tmpTypes[i].Substring(0, 1);
                        treq.SmartcardData = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                        i++;
                    }
                }

                parameterAndValue notesType = new parameterAndValue();
                List<parameterAndValue> notesTypeList = new List<parameterAndValue>();
                treq.NotesType = false;

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "w")
                {
                    sql = @"SELECT [optionNum] FROM [enhancedParams] " +
                                    "WHERE [optionCode] ='45'";
                    /**
                     * if 45 is 000, 2 digit, if it is 001, 3 digit 
                     * **/
                    Dictionary<string, string> resultData = readData(sql);

                    treq.DeviceIdw = tmpTypes[i].Substring(0, 1);

                    Utility u = new Utility();

                    string optionNum = "00000000";
                    int digitPad = 2;

                    foreach (string item in resultData.Values)
                    {
                        optionNum = u.dec2bin(item, 8);
                    }

                    if (optionNum.Substring(1, 1) == "1")
                    {
                        // use three digit
                        digitPad = 3;
                    }

                    int entries = (tmpTypes[i].Length - 1) / (digitPad + 2);
                    int offset = 1;

                    for (int z = 0; z < entries; z++)
                    {
                        notesType.paramName = tmpTypes[i].Substring(offset, 2);
                        offset = offset + 2;
                        notesType.paramValue = tmpTypes[i].Substring(offset, digitPad);
                        offset = offset + digitPad;
                        notesTypeList.Add(notesType);
                    }
                    treq.NotesType = true;
                }

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "a")

                {
                    treq.DocumentDataId = tmpTypes[i].Substring(0, 1);
                    treq.MICRDetected1 = tmpTypes[i].Substring(1, 1);
                    treq.MICRValue1 = tmpTypes[i].Substring(2, tmpTypes[i].Length - 2);
                }

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "e")
                {
                    treq.FieldIdE = tmpTypes[i].Substring(0, 1);
                    treq.BarcodeFormatId = tmpTypes[i].Substring(1, 4);
                    treq.Reserved = tmpTypes[i].Substring(5, 2);
                    treq.ScanBarcodeData = tmpTypes[i].Substring(7, tmpTypes[i].Length - 7);
                }

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "f")

                {
                    treq.FieldIDf = tmpTypes[i].Substring(0, 1);
                    int numberOfCoinfromHopper = (tmpTypes[i].Length - 1) / 2;

                    if (numberOfCoinfromHopper > 0)
                    { treq.NumberOfCoinfromHopper1 = tmpTypes[i].Substring(1, 2); }

                    if (numberOfCoinfromHopper > 1)
                    { treq.NumberOfCoinfromHopper2 = tmpTypes[i].Substring(3, 2); }

                    if (numberOfCoinfromHopper > 2)
                    { treq.NumberOfCoinfromHopper3 = tmpTypes[i].Substring(5, 2); }

                    if (numberOfCoinfromHopper > 3)
                    { treq.NumberOfCoinfromHopper4 = tmpTypes[i].Substring(7, 2); }

                    if (numberOfCoinfromHopper > 4)
                    { treq.NumberOfCoinfromHopper5 = tmpTypes[i].Substring(9, 2); }

                    if (numberOfCoinfromHopper > 5)
                    { treq.NumberOfCoinfromHopper6 = tmpTypes[i].Substring(11, 2); }

                    if (numberOfCoinfromHopper > 6)
                    { treq.NumberOfCoinfromHopper7 = tmpTypes[i].Substring(13, 2); }

                    if (numberOfCoinfromHopper > 7)
                    { treq.NumberOfCoinfromHopper8 = tmpTypes[i].Substring(15, 2); }
                }

                List<depCurrency> depositCurrencies = new List<depCurrency>();
                List<checks> depositChecks = new List<checks>();

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "g")
                {
                    treq.DataIDg = tmpTypes[i].Substring(0, 1);
                    treq.TotalChecksToReturn = tmpTypes[i].Substring(1, 3);
                    treq.Zero41 = tmpTypes[i].Substring(4, 4);
                    string[] tmpCurrencies;
                    tmpCurrencies = tmpTypes[i].Substring(8, tmpTypes[i].Length - 8).Split((char)0x1d);

                    bool currencyFlag = true;
                    int amountReached = 0;
                    int amountCheque = 0;

                    foreach (string currencyChecks in tmpCurrencies)
                    {
                        int offset2 = 0;
                        depCurrency curr = new depCurrency();
                        checks cheques = new checks();
                        if (currencyFlag)
                        {
                            curr.DepositCurrency = currencyChecks.Substring(0, 3);
                            curr.AmountExponentSign = currencyChecks.Substring(3, 1);
                            curr.AmountExponentValue = currencyChecks.Substring(4, 2);
                            curr.TotalCustomerAmount = currencyChecks.Substring(6, 12);
                            curr.TotalDeriveAmount = currencyChecks.Substring(18, 12);
                            curr.Zero42 = currencyChecks.Substring(30, 4);
                            amountReached = Convert.ToInt32(curr.TotalDeriveAmount);
                            offset2 = 34;
                        }
                        cheques.depositCurrency = curr.DepositCurrency;
                        cheques.checkId = currencyChecks.Substring(offset2, 3);
                        offset2 = offset2 + 3;
                        cheques.customerCheckAmount = currencyChecks.Substring(offset2, 12);
                        offset2 = offset2 + 12;
                        cheques.deriveCheckAmount = currencyChecks.Substring(offset2, 12);
                        offset2 = offset2 + 12;
                        cheques.codelineLength = currencyChecks.Substring(offset2, 3);
                        offset2 = offset2 + 3;
                        amountCheque = Convert.ToInt32(cheques.deriveCheckAmount);
                        int offset = Convert.ToInt32(cheques.codelineLength);
                        cheques.codelineData = currencyChecks.Substring(offset2, offset);
                        depositChecks.Add(cheques);
                        curr.NumOfChecks = depositChecks.Count;

                        if (amountReached == amountCheque)
                        {
                            currencyFlag = true;
                            depositCurrencies.Add(curr);
                        }
                        else
                        {
                            currencyFlag = false;
                            amountReached = amountReached - amountCheque;
                        }
                    }
                    treq.NumberOfCurrencies = depositCurrencies.Count;

                }

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "<")
                {
                    treq.FieldIdLessThan = tmpTypes[i].Substring(0, 1);
                    treq.VGLanguageId1 = tmpTypes[i].Substring(1, 2);
                }

                // future expansion

                //if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                //if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "?")

                //{
                //    treq.optionalDataFields = tmpTypes[i].Substring(0, 1);
                //    treq.optionalData = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                //}

                if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                if (tmpTypes.Length > i && tmpTypes[i].Length > 0)
                {
                    treq.MAC1 = tmpTypes[i].Substring(0, tmpTypes[i].Length);
                }

                loadNum++;

                ///
                /// 1. insert parent record to treq table
                ///
                sql = @"INSERT INTO treq([logkey],[rectype],[messageClass],[messageSubclass],[luno],[timeVariantNumber] " +
                      ",[topOfReceiptFlag],[messageCoordinationNumber],[track2Data] " +
                      ",[track3Data],[operationCodeData],[amountEntryField],[PINBuffer] " +
                      ",[BufferB],[BufferC],[track1Identier],[track1Data],[transactionStatusDataID] " +
                      ",[lastTransactionStatusSerialNumber],[lastTransactionStatusLastStatusIssued] " +
                      ",[lastTransactionStatusNotesDispensedData],[lastTransactionStatusCoinageAmountDispensed]" +
                      ",[lastTransactionStatusCoinsDispensedData],[lastTransactionStatusCashDepositData] " +
                      ",[CSPDataIdU],[CSPDataU],[confirmationCSPDataIdV],[confirmationCSPDataV] " +
                      ",[vcDataIdW],[vcDataW],[vcDataIdX],[vcDataX],[vcDataIdY],[vcDataY],[vcDataIdZ] " +
                      ",[vcDataZ],[vcDataIDBracket],[vcDataBracket],[vcDataIDSlash],[vcDataSlash] " +
                      ",[smartcardID5],[smartcardData],[deviceIdw],[notesType],[documentDataId],[MICRDetected] " +
                      ",[MICRValue],[fieldIdE],[barcodeFormatId],[reserved],[scanBarcodeData],[fieldIDf] " +
                      ",[numberOfCoinfromHopper1],[numberOfCoinfromHopper2],[numberOfCoinfromHopper3],[numberOfCoinfromHopper4] " +
                      ",[numberOfCoinfromHopper5],[numberOfCoinfromHopper6],[numberOfCoinfromHopper7],[numberOfCoinfromHopper8] " +
                      ",[dataIDg],[totalChecksToReturn],[zero41],[numberOfCurrencies],[fieldIdLessThan] " +
                      ",[VGLanguageId],[optionalDataFields],[optionalData],[MAC],[prjkey],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" +
                                   'T' + "','" +
                                   treq.MessageClass + "','" +
                                   treq.MessageSubclass + "','" +
                                   treq.Luno + "','" +
                                   treq.TimeVariantNumber + "','" +
                                   treq.TopOfReceiptFlag + "','" +
                                   treq.MessageCoordinationNumber + "','" +
                                   treq.Track2Data + "','" +
                                   treq.Track3Data + "','" +
                                   treq.OperationCodeData + "','" +
                                   treq.AmountEntryField + "','" +
                                   treq.PINBuffer1 + "','" +
                                   treq.BufferB1 + "','" +
                                   treq.BufferC1 + "','" +
                                   treq.Track1Identier + "','" +
                                   treq.Track1Data + "','" +
                                   treq.TransactionStatusDataID + "','" +
                                   treq.LastTransactionStatusData.SerialNumber + "','" +
                                   treq.LastTransactionStatusData.LastStatusIssued + "','" +
                                   treq.LastTransactionStatusData.LastTransactionNotesDispenseData + "','" +
                                   treq.LastTransactionStatusData.LastTransactionCoinageAmountDispensed + "','" +
                                   treq.LastTransactionStatusData.LastTransactionCoinsDispenseData + "','" +
                                   treq.LastTransactionStatusData.LastTransactionCashDepositData + "','" +
                                   treq.CSPDataIdU1 + "','" +
                                   treq.CSPDataU1 + "','" +
                                   treq.ConfirmationCSPDataIdV + "','" +
                                   treq.ConfirmationCSPDataV + "','" +
                                   treq.VcDataIdW + "','" +
                                   treq.VcDataW + "','" +
                                   treq.VcDataIdX + "','" +
                                   treq.VcDataX + "','" +
                                   treq.VcDataIdY + "','" +
                                   treq.VcDataY + "','" +
                                   treq.VcDataIdZ + "','" +
                                   treq.VcDataZ + "','" +
                                   treq.VcDataIDBracket + "','" +
                                   treq.VcDataBracket + "','" +
                                   treq.VcDataIDSlash + "','" +
                                   treq.VcDataSlash + "','" +
                                   treq.SmartcardID5 + "','" +
                                   treq.SmartcardData + "','" +
                                   treq.DeviceIdw + "','" +
                                   treq.NotesType + "','" +
                                   treq.DocumentDataId + "','" +
                                   treq.MICRDetected1 + "','" +
                                   treq.MICRValue1 + "','" +
                                   treq.FieldIdE + "','" +
                                   treq.BarcodeFormatId + "','" +
                                   treq.Reserved + "','" +
                                   treq.ScanBarcodeData + "','" +
                                   treq.FieldIDf + "','" +
                                   treq.NumberOfCoinfromHopper1 + "','" +
                                   treq.NumberOfCoinfromHopper2 + "','" +
                                   treq.NumberOfCoinfromHopper3 + "','" +
                                   treq.NumberOfCoinfromHopper4 + "','" +
                                   treq.NumberOfCoinfromHopper5 + "','" +
                                   treq.NumberOfCoinfromHopper6 + "','" +
                                   treq.NumberOfCoinfromHopper7 + "','" +
                                   treq.NumberOfCoinfromHopper8 + "','" +
                                   treq.DataIDg + "','" +
                                   treq.TotalChecksToReturn + "','" +
                                   treq.Zero41 + "','" +
                                   depositCurrencies.Count.ToString() + "','" +
                                   treq.FieldIdLessThan + "','" +
                                   treq.VGLanguageId1 + "','" +
                                   treq.OptionalDataFields + "','" +
                                   treq.OptionalData + "','" +
                                   treq.MAC1 + "','" + key + "'," + logID + ")";


                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;

                /// 
                /// 2. insert option record to treqOptions table
                /// 3. insert depositCurrencies record to treqCurrencies
                /// 4. insert depostChecks records to treqChecks
                /// 
                /// *//
                /// id int not null identity primary key,
                foreach (depCurrency c in depositCurrencies)
                {
                    sql = @"INSERT INTO treqCurrencys([logkey],[depositCurrency],[amountExponentSign]," +
                      "[amountExponentValue],[totalCustomerAmount],[totalDeriveAmount],[zeroes1],[numberOfChecks],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" +
                                   c.DepositCurrency + "','" +
                                   c.AmountExponentSign + "','" +
                                   c.AmountExponentValue + "','" +
                                   c.TotalCustomerAmount + "','" +
                                   c.TotalDeriveAmount + "','" +
                                   c.Zero42 + "','" +
                                   c.NumOfChecks + "'," +
                                   logID + ")";

                    if (db.crudToDb(sql) == false)
                        return false;
                }

                foreach (checks c in depositChecks)
                {
                    sql = @"INSERT INTO treqChecks([logkey],[depositCurrency],[checkId]," +
                    "[customerCheckAmount],[derivedCheckAmount],[codelineLength],[codelineData],[logID]) " +
                    " VALUES('" + r.typeIndex + "','" +
                                 c.depositCurrency + "','" +
                                 c.checkId + "','" +
                                 c.customerCheckAmount + "','" +
                                 c.deriveCheckAmount + "','" +
                                 c.codelineLength + "','" +
                                 c.codelineData + "'," +
                                 logID + ")";

                    if (db.crudToDb(sql) == false)
                        return false;
                }

                foreach (parameterAndValue c in notesTypeList)
                {
                    sql = @"INSERT INTO treqOptions([logkey],[fieldOption],[optionName],[optionValue],[logID]) " +
                    " VALUES('" + r.typeIndex + "','" +
                                 "01" + "','" +
                                 c.paramName + "','" +
                                 c.paramValue + "'," +
                                 logID + ")";

                    if (db.crudToDb(sql) == false)
                        return false;
                }
            }
            return true;
        }


        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();

            string sql = @"SELECT TOP 1 * from treq WHERE logID = '" + logID + "' AND prjkey = '" + projectKey + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            sql = @"SELECT id, logkey,fieldOption as ""field Option"", optionName as ""Option Name"", optionValue as ""Option Value"", logID from treqOptions WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            sql = @"SELECT id, logkey, depositCurrency as ""Deposit Currency"", amountExponentSign as ""Amount Exponent Sign"", amountExponentValue as ""Amount Exponent Value"", totalCustomerAmount as ""Total Customer Amount"", totalDeriveAmount as ""Total Derive Amount"", zeroes1, numberOfChecks as ""Number of Cheques"", logID from treqCurrencies WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            sql = @"SELECT id, logkey, depositCurrency as ""Deposit Currency"", checkId as ""Check ID"", customerCheckAmount as ""Customer Check Amount"", derivedCheckAmount as ""Derived Check Amount"", codelineLength as ""Code Line Length"", codelineData as ""Code Line Data"", logID from treqChecks WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = new List<DataTable>();
            dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable tReqDt = getDescription();

            if (dts[0].Rows.Count > 0)
            {
                for (int rowNum = 0; rowNum < dts[0].Rows.Count; rowNum++)
                {
                    for (int field = 3; field <= dts[0].Rows[rowNum].ItemArray.Length - 3; field++)
                    {
                        if (field == 42)
                        {
                            if (dts[1].Rows.Count > 0)
                            {
                                txtField += getTreqOptions(dts[1]);
                            }
                            continue;
                        }
                        if (field == 62)
                        {
                            if (dts[2].Rows.Count > 0)
                            {
                                txtField += getTreqCurrencies(dts[2]);
                            }
                            if (dts[3].Rows.Count > 0)
                            {
                                txtField += getTreqCheques(dts[3]);
                            }

                            continue;
                        }
                        string fieldContent = dts[0].Rows[rowNum].ItemArray[field].ToString().Trim();
                        if (fieldContent != "")
                        {
                            txtField += getOptionDescription(tReqDt, field.ToString("00"), fieldContent);
                        }
                    }
                }
            }
            return txtField;
        }

        // mlh NOT TESTED

        private string getTreqOptions(DataTable dt)
        {
            string notesType = "";
            if (dt.Rows.Count > 0)
            {
                for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                {
                    for (int fieldNum = 2; fieldNum < dt.Columns.Count - 1; fieldNum++)
                    {
                        notesType += dt.Columns[fieldNum].ColumnName.Trim() + "\t = " + dt.Rows[rowNum][fieldNum].ToString() + System.Environment.NewLine;
                    }
                }
            }
            return notesType;
        }

        // mlh NOT TESTED

        private string getTreqCurrencies(DataTable dt)
        {
            string currencies = "";
            if (dt.Rows.Count > 0)
            {
                for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                {
                    for (int fieldNum = 2; fieldNum < dt.Columns.Count - 1; fieldNum++)
                    {
                        if (fieldNum != 7)
                            currencies += dt.Columns[fieldNum].ColumnName.Trim() + "\t = " + dt.Rows[rowNum][fieldNum].ToString() + System.Environment.NewLine;
                    }
                }
            }
            return currencies;
        }

        // mlh NOT TESTED

        private string getTreqCheques(DataTable dt)
        {
            string checks = "";
            if (dt.Rows.Count > 0)
            {
                for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                {
                    for (int fieldNum = 2; fieldNum < dt.Columns.Count - 1; fieldNum++)
                    {
                        checks += dt.Columns[fieldNum].ColumnName.Trim() + "\t = " + dt.Rows[rowNum][fieldNum].ToString() + System.Environment.NewLine;
                    }
                }
            }
            return checks;
        }

    }
}