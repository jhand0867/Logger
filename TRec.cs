using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logger
{
    public struct lastTransactionStatus
    {
        public string serialNumber;
        public string lastStatusIssued;
        public string lastTransactionNotesDispenseData;
        public string lastTransactionCoinageAmountDispensed;
        public string lastTransactionCoinsDispenseData;
        public string lastTransactionCashDepositData;
    }

    public struct depCurrency
    {
        public string depositCurrency;
        public string amountExponentSign;
        public string amountExponentValue;
        public string totalCustomerAmount;
        public string totalDeriveAmount;
        public string zero42;
        public int numOfChecks;
    };

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
        public string luno;
        public string timeVariantNumber;
        public string topOfReceiptFlag;
        public string messageCoordinationNumber;
        public string track2Data;
        public string track3Data;
        public string operationCodeData;
        public string amountEntryField;
        public string PINBuffer;
        public string BufferB;
        public string BufferC;
        public string track1Identier;
        public string track1Data;
        public string transactionStatusDataID;
        public lastTransactionStatus lastTransactionStatusData;
        public string CSPDataIdU;
        public string CSPDataU;
        public string confirmationCSPDataIdV;
        public string confirmationCSPDataV;
        public string vcDataIdW;
        public string vcDataW;
        public string vcDataIdX;
        public string vcDataX;
        public string vcDataIdY;
        public string vcDataY;
        public string vcDataIdZ;
        public string vcDataZ;
        public string vcDataIDBracket;
        public string vcDataBracket;
        public string vcDataIDSlash;
        public string vcDataSlash;
        public string smartcardID5;
        public string smartcardData;
        public string deviceIdw;
        public bool notesType;
        public string documentDataId;
        public string MICRDetected;
        public string MICRValue;
        public string fieldIdE;
        public string barcodeFormatId;
        public string reserved;
        public string scanBarcodeData;
        public string fieldIDf;
        public string numberOfCoinfromHopper1;
        public string numberOfCoinfromHopper2;
        public string numberOfCoinfromHopper3;
        public string numberOfCoinfromHopper4;
        public string numberOfCoinfromHopper5;
        public string numberOfCoinfromHopper6;
        public string numberOfCoinfromHopper7;
        public string numberOfCoinfromHopper8;
        public string dataIDg;
        public string totalChecksToReturn;
        public string zero41;
        public int numberOfCurrencies;
        public string fieldIdLessThan;
        public string VGLanguageId;
        public string optionalDataFields;
        public string optionalData;
        public string MAC;
    };
    public struct parameterAndValue
    {
        public string paramName;
        public string paramValue;
    };

    class TRec : App
    {
        public DataTable getDescription()
        {
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            DataTable dt = new DataTable();

            try
            {
                cnn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [dataDescription] WHERE recType = '" + "T" + "'", cnn))
                {
                    sda.Fill(dt);
                }
            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());
                return null;
            }

            return dt;
        }

        public bool writeData(List<typeRec> typeRecs, string key, string logID)
        {
            string[] tmpTypes;
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                String sql = "";
                int loadNum = 0;

                foreach (typeRec r in typeRecs)
                {
                    tmpTypes = r.typeContent.Split((char)0x1c);

                    transactionRequest treq = new transactionRequest();
                    treq.luno = tmpTypes[1];

                    treq.timeVariantNumber = tmpTypes[3];
                    treq.topOfReceiptFlag = tmpTypes[4].Substring(0, 1);
                    treq.messageCoordinationNumber = tmpTypes[4].Substring(1, 1);
                    treq.track2Data = tmpTypes[5];
                    treq.track3Data = tmpTypes[6];
                    treq.operationCodeData = tmpTypes[7];
                    treq.amountEntryField = tmpTypes[8];
                    treq.PINBuffer = tmpTypes[9];
                    treq.BufferB = tmpTypes[10];
                    treq.BufferC = tmpTypes[11];

                    int i = 12;

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                        tmpTypes[i].Substring(0, 1) == "1")
                    {
                        treq.track1Identier = tmpTypes[i].Substring(0, 1);
                        treq.track1Data = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
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

                        treq.transactionStatusDataID = tmpTypes[i].Substring(0, 1);
                        treq.lastTransactionStatusData.serialNumber = tmpTypes[i].Substring(1, 4);
                        treq.lastTransactionStatusData.lastStatusIssued = tmpTypes[i].Substring(5, 1);

                        if ((resultData.Count == 0) ||
                           (resultData.Count > 0 && resultData.ContainsValue("000")))
                        {
                            if (tmpTypes[i].Length > 25)
                            {
                                treq.lastTransactionStatusData.lastTransactionNotesDispenseData =
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
                                treq.lastTransactionStatusData.lastTransactionNotesDispenseData =
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
                            treq.lastTransactionStatusData.lastTransactionCoinageAmountDispensed =
                            tmpTypes[i].Substring(fourOrSeven, 5);

                            fourOrSeven = fourOrSeven + 5;
                            treq.lastTransactionStatusData.lastTransactionCoinsDispenseData =
                                tmpTypes[i].Substring(fourOrSeven, 20);

                            fourOrSeven = fourOrSeven + 20;
                            treq.lastTransactionStatusData.lastTransactionCashDepositData =
                                tmpTypes[i].Substring(fourOrSeven, tmpTypes[i].Length - fourOrSeven);
                        }
                        i++;
                    }

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Length > 0 &&
                        tmpTypes[i].Substring(0, 1) == "U")
                    {
                        treq.CSPDataIdU = tmpTypes[i].Substring(0, 1);
                        treq.CSPDataU = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                        i++;
                    }

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "V")
                    {
                        treq.confirmationCSPDataIdV = tmpTypes[i].Substring(0, 1);
                        treq.confirmationCSPDataV = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                        i++;
                    }

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "W")
                    {
                        treq.vcDataIdW = tmpTypes[i].Substring(0, 1);
                        treq.vcDataW = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                        i++;
                    }

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "X")
                    {
                        treq.vcDataIdX = tmpTypes[i].Substring(0, 1);
                        treq.vcDataX = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                        i++;
                    }

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "Y")
                    {
                        treq.vcDataIdY = tmpTypes[i].Substring(0, 1);
                        treq.vcDataY = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                        i++;
                    }

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "Z")
                    {
                        treq.vcDataIdZ = tmpTypes[i].Substring(0, 1);
                        treq.vcDataZ = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                        i++;
                    }

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "[")
                    {
                        treq.vcDataIDBracket = tmpTypes[i].Substring(0, 1);
                        treq.vcDataBracket = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                        i++;
                    }

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == @"\")
                    {
                        treq.vcDataIDSlash = tmpTypes[i].Substring(0, 1);
                        treq.vcDataSlash = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                        i++;
                    }

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "5")
                    {
                        if (tmpTypes[i].Substring(0, 4) == "5CAM")
                        {
                            treq.smartcardID5 = tmpTypes[i].Substring(0, 1);
                            treq.smartcardData = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);
                            i++;
                        }
                    }

                    parameterAndValue notesType = new parameterAndValue();
                    List<parameterAndValue> notesTypeList = new List<parameterAndValue>();
                    treq.notesType = false;

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "w")
                    {
                        sql = @"SELECT [optionNum] FROM [enhancedParams] " +
                                        "WHERE [optionCode] ='45'";
                        /**
                         * if 45 is 000, 2 digit, if it is 001, 3 digit 
                         * **/
                        Dictionary<string, string> resultData = readData(sql);

                        treq.deviceIdw = tmpTypes[i].Substring(0, 1);

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

                        int entries = (tmpTypes[i].Length - 1) / digitPad + 2;
                        int offset = 1;

                        for (int z = 0; z < entries; z++)
                        {
                            notesType.paramName = tmpTypes[i].Substring(offset, 2);
                            offset = offset + 2;
                            notesType.paramValue = tmpTypes[i].Substring(offset, digitPad);
                            offset = offset + digitPad;
                            notesTypeList.Add(notesType);
                        }
                        treq.notesType = true;
                    }

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "a")

                    {
                        treq.documentDataId = tmpTypes[i].Substring(0, 1);
                        treq.MICRDetected = tmpTypes[i].Substring(1, 1);
                        treq.MICRValue = tmpTypes[i].Substring(2, tmpTypes[i].Length - 2);
                    }

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "e")
                    {
                        treq.fieldIdE = tmpTypes[i].Substring(0, 1);
                        treq.barcodeFormatId = tmpTypes[i].Substring(1, 4);
                        treq.reserved = tmpTypes[i].Substring(5, 2);
                        treq.scanBarcodeData = tmpTypes[i].Substring(7, tmpTypes[i].Length - 7);
                    }

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "f")

                    {
                        treq.fieldIDf = tmpTypes[i].Substring(0, 1);
                        int numberOfCoinfromHopper = (tmpTypes[i].Length - 1) / 2;

                        if (numberOfCoinfromHopper > 0)
                        { treq.numberOfCoinfromHopper1 = tmpTypes[i].Substring(1, 2); }

                        if (numberOfCoinfromHopper > 1)
                        { treq.numberOfCoinfromHopper2 = tmpTypes[i].Substring(3, 2); }

                        if (numberOfCoinfromHopper > 2)
                        { treq.numberOfCoinfromHopper3 = tmpTypes[i].Substring(5, 2); }

                        if (numberOfCoinfromHopper > 3)
                        { treq.numberOfCoinfromHopper4 = tmpTypes[i].Substring(7, 2); }

                        if (numberOfCoinfromHopper > 4)
                        { treq.numberOfCoinfromHopper5 = tmpTypes[i].Substring(9, 2); }

                        if (numberOfCoinfromHopper > 5)
                        { treq.numberOfCoinfromHopper6 = tmpTypes[i].Substring(11, 2); }

                        if (numberOfCoinfromHopper > 6)
                        { treq.numberOfCoinfromHopper7 = tmpTypes[i].Substring(13, 2); }

                        if (numberOfCoinfromHopper > 7)
                        { treq.numberOfCoinfromHopper8 = tmpTypes[i].Substring(15, 2); }
                    }

                    List<depCurrency> depositCurrencies = new List<depCurrency>();
                    List<checks> depositChecks = new List<checks>();

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "g")
                    {
                        treq.dataIDg = tmpTypes[i].Substring(0, 1);
                        treq.totalChecksToReturn = tmpTypes[i].Substring(1, 3);
                        treq.zero41 = tmpTypes[i].Substring(4, 4);
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
                                curr.depositCurrency = currencyChecks.Substring(0, 3);
                                curr.amountExponentSign = currencyChecks.Substring(3, 1);
                                curr.amountExponentValue = currencyChecks.Substring(4, 2);
                                curr.totalCustomerAmount = currencyChecks.Substring(6, 12);
                                curr.totalDeriveAmount = currencyChecks.Substring(18, 12);
                                curr.zero42 = currencyChecks.Substring(30, 4);
                                amountReached = Convert.ToInt32(curr.totalDeriveAmount);
                                offset2 = 34;
                            }
                            cheques.depositCurrency = curr.depositCurrency;
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
                            curr.numOfChecks = depositChecks.Count;

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
                        treq.numberOfCurrencies = depositCurrencies.Count;

                    }

                    if (tmpTypes.Length > i && tmpTypes[i].Length == 0) { i++; }

                    if (tmpTypes.Length > i && tmpTypes[i].Substring(0, 1) == "<")
                    {
                        treq.fieldIdLessThan = tmpTypes[i].Substring(0, 1);
                        treq.VGLanguageId = tmpTypes[i].Substring(1, 2);
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
                        treq.MAC = tmpTypes[i].Substring(0, tmpTypes[i].Length);
                    }

                    loadNum++;

                    ///
                    /// 1. insert parent record to treq table
                    ///
                    sql = @"INSERT INTO treq([logkey],[rectype],[luno],[timeVariantNumber] " +
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
                                       treq.luno + "','" +
                                       treq.timeVariantNumber + "','" +
                                       treq.topOfReceiptFlag + "','" +
                                       treq.messageCoordinationNumber + "','" +
                                       treq.track2Data + "','" +
                                       treq.track3Data + "','" +
                                       treq.operationCodeData + "','" +
                                       treq.amountEntryField + "','" +
                                       treq.PINBuffer + "','" +
                                       treq.BufferB + "','" +
                                       treq.BufferC + "','" +
                                       treq.track1Identier + "','" +
                                       treq.track1Data + "','" +
                                       treq.transactionStatusDataID + "','" +
                                       treq.lastTransactionStatusData.serialNumber + "','" +
                                       treq.lastTransactionStatusData.lastStatusIssued + "','" +
                                       treq.lastTransactionStatusData.lastTransactionNotesDispenseData + "','" +
                                       treq.lastTransactionStatusData.lastTransactionCoinageAmountDispensed + "','" +
                                       treq.lastTransactionStatusData.lastTransactionCoinsDispenseData + "','" +
                                       treq.lastTransactionStatusData.lastTransactionCashDepositData + "','" +
                                       treq.CSPDataIdU + "','" +
                                       treq.CSPDataU + "','" +
                                       treq.confirmationCSPDataIdV + "','" +
                                       treq.confirmationCSPDataV + "','" +
                                       treq.vcDataIdW + "','" +
                                       treq.vcDataW + "','" +
                                       treq.vcDataIdX + "','" +
                                       treq.vcDataX + "','" +
                                       treq.vcDataIdY + "','" +
                                       treq.vcDataY + "','" +
                                       treq.vcDataIdZ + "','" +
                                       treq.vcDataZ + "','" +
                                       treq.vcDataIDBracket + "','" +
                                       treq.vcDataBracket + "','" +
                                       treq.vcDataIDSlash + "','" +
                                       treq.vcDataSlash + "','" +
                                       treq.smartcardID5 + "','" +
                                       treq.smartcardData + "','" +
                                       treq.deviceIdw + "','" +
                                       treq.notesType + "','" +
                                       treq.documentDataId + "','" +
                                       treq.MICRDetected + "','" +
                                       treq.MICRValue + "','" +
                                       treq.fieldIdE + "','" +
                                       treq.barcodeFormatId + "','" +
                                       treq.reserved + "','" +
                                       treq.scanBarcodeData + "','" +
                                       treq.fieldIDf + "','" +
                                       treq.numberOfCoinfromHopper1 + "','" +
                                       treq.numberOfCoinfromHopper2 + "','" +
                                       treq.numberOfCoinfromHopper3 + "','" +
                                       treq.numberOfCoinfromHopper4 + "','" +
                                       treq.numberOfCoinfromHopper5 + "','" +
                                       treq.numberOfCoinfromHopper6 + "','" +
                                       treq.numberOfCoinfromHopper7 + "','" +
                                       treq.numberOfCoinfromHopper8 + "','" +
                                       treq.dataIDg + "','" +
                                       treq.totalChecksToReturn + "','" +
                                       treq.zero41 + "','" +
                                       depositCurrencies.Count.ToString() + "','" +
                                       treq.fieldIdLessThan + "','" +
                                       treq.VGLanguageId + "','" +
                                       treq.optionalDataFields + "','" +
                                       treq.optionalData + "','" +
                                       treq.MAC + "','" + key + "'," + logID + ")";

                    command = new SqlCommand(sql, cnn);
                    dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                    dataAdapter.InsertCommand.ExecuteNonQuery();
                    command.Dispose();
                    // cnn.Close();


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
                                       c.depositCurrency + "','" +
                                       c.amountExponentSign + "','" +
                                       c.amountExponentValue + "','" +
                                       c.totalCustomerAmount + "','" +
                                       c.totalDeriveAmount + "','" +
                                       c.zero42 + "','" +
                                       c.numOfChecks + "'," +
                                       logID + ")";

                        command = new SqlCommand(sql, cnn);
                        dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                        dataAdapter.InsertCommand.ExecuteNonQuery();
                        command.Dispose();
                        // cnn.Close();
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

                        command = new SqlCommand(sql, cnn);
                        dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                        dataAdapter.InsertCommand.ExecuteNonQuery();
                        command.Dispose();
                        // cnn.Close();
                    }

                    foreach (parameterAndValue c in notesTypeList)
                    {
                        sql = @"INSERT INTO treqOptions([logkey],[fieldOption],[optionName],[optionValue],[logID]) " +
                        " VALUES('" + r.typeIndex + "','" +
                                     "01" + "','" +
                                     c.paramName + "','" +
                                     c.paramValue + "'," +
                                     logID + ")";

                        command = new SqlCommand(sql, cnn);
                        dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                        dataAdapter.InsertCommand.ExecuteNonQuery();
                        command.Dispose();
                        // cnn.Close();
                    }
                }
                cnn.Close();
                return true;
            }

            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());
                return false;

            }

        }


        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
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

                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT TOP 1 * from treq WHERE logID = '" + logID + "' AND prjkey = '" + projectKey + "' AND logkey LIKE '" + logKey + "%'", cnn))
                {
                    sda.Fill(dt);
                }
                dts.Add(dt);

                dt = new DataTable();
                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT id, logkey,fieldOption as ""field Option"", optionName as ""Option Name"", optionValue as ""Option Value"", logID from treqOptions WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'", cnn))
                 {
                    sda.Fill(dt);
                }
                dts.Add(dt);

                dt = new DataTable();
                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT id, logkey, depositCurrency as ""Deposit Currency"", amountExponentSign as ""Amount Exponent Sign"", amountExponentValue as ""Amount Exponent Value"", totalCustomerAmount as ""Total Customer Amount"", totalDeriveAmount as ""Total Derive Amount"", zeroes1, numberOfChecks as ""Number of Cheques"", logID from treqCurrencies WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'", cnn))
                {
                    sda.Fill(dt);
                }
                dts.Add(dt);

                dt = new DataTable();
                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT id, logkey, depositCurrency as ""Deposit Currency"", checkId as ""Check ID"", customerCheckAmount as ""Customer Check Amount"", derivedCheckAmount as ""Derived Check Amount"", codelineLength as ""Code Line Length"", codelineData as ""Code Line Data"", logID from treqChecks WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'", cnn))
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

    }
}
