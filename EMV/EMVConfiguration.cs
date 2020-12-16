using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct iccCurrency
    {
        private string rectype;
        private string currencyType;
        private string responseFormat;
        private string responseLength;
        private string trCurrencyCodeTag;
        private string trCurrencyCodeLgth;
        private string trCurrencyCodeValue;
        private string trCurrencyExpTag;
        private string trCurrencyExpLgth;
        private string trCurrencyExpValue;

        public string Rectype { get => rectype; set => rectype = value; }
        public string CurrencyType { get => currencyType; set => currencyType = value; }
        public string ResponseFormat { get => responseFormat; set => responseFormat = value; }
        public string ResponseLength { get => responseLength; set => responseLength = value; }
        public string TrCurrencyCodeTag { get => trCurrencyCodeTag; set => trCurrencyCodeTag = value; }
        public string TrCurrencyCodeLgth { get => trCurrencyCodeLgth; set => trCurrencyCodeLgth = value; }
        public string TrCurrencyCodeValue { get => trCurrencyCodeValue; set => trCurrencyCodeValue = value; }
        public string TrCurrencyExpTag { get => trCurrencyExpTag; set => trCurrencyExpTag = value; }
        public string TrCurrencyExpLgth { get => trCurrencyExpLgth; set => trCurrencyExpLgth = value; }
        public string TrCurrencyExpValue { get => trCurrencyExpValue; set => trCurrencyExpValue = value; }
    }
    struct iccTransaction
    {
        private string rectype;
        private string transactionType;
        private string responseFormat;
        private string responseLength;
        private string transactionTypeTag;
        private string transactionTypeLgth;
        private string transactionTypeValue;
        private string transactionCatCodeTag;
        private string transactionCatCodeLgth;
        private string transactionCatCodeValue;

        public string Rectype { get => rectype; set => rectype = value; }
        public string TransactionType { get => transactionType; set => transactionType = value; }
        public string ResponseFormat { get => responseFormat; set => responseFormat = value; }
        public string ResponseLength { get => responseLength; set => responseLength = value; }
        public string TransactionTypeTag { get => transactionTypeTag; set => transactionTypeTag = value; }
        public string TransactionTypeLgth { get => transactionTypeLgth; set => transactionTypeLgth = value; }
        public string TransactionTypeValue { get => transactionTypeValue; set => transactionTypeValue = value; }
        public string TransactionCatCodeTag { get => transactionCatCodeTag; set => transactionCatCodeTag = value; }
        public string TransactionCatCodeLgth { get => transactionCatCodeLgth; set => transactionCatCodeLgth = value; }
        public string TransactionCatCodeValue { get => transactionCatCodeValue; set => transactionCatCodeValue = value; }
    };
    struct iccLanguage
    {
        private string rectype;
        private string languageCode;
        private string screenBase;
        private string audioBase;
        private string opCodeBufferPositions;
        private string opCodeBufferValues;

        public string Rectype { get => rectype; set => rectype = value; }
        public string LanguageCode { get => languageCode; set => languageCode = value; }
        public string ScreenBase { get => screenBase; set => screenBase = value; }
        public string AudioBase { get => audioBase; set => audioBase = value; }
        public string OpCodeBufferPositions { get => opCodeBufferPositions; set => opCodeBufferPositions = value; }
        public string OpCodeBufferValues { get => opCodeBufferValues; set => opCodeBufferValues = value; }
    };
    struct iccTerminal
    {
        private string rectype;
        private string responseFormat;
        private string responseLength;
        private string terCountryCodeTag;
        private string terCountryCodeLgth;
        private string terCountryCodeValue;
        private string terTypeTag;
        private string terTypeLgth;
        private string terTypeValue;

        public string Rectype { get => rectype; set => rectype = value; }
        public string ResponseFormat { get => responseFormat; set => responseFormat = value; }
        public string ResponseLength { get => responseLength; set => responseLength = value; }
        public string TerCountryCodeTag { get => terCountryCodeTag; set => terCountryCodeTag = value; }
        public string TerCountryCodeLgth { get => terCountryCodeLgth; set => terCountryCodeLgth = value; }
        public string TerCountryCodeValue { get => terCountryCodeValue; set => terCountryCodeValue = value; }
        public string TerTypeTag { get => terTypeTag; set => terTypeTag = value; }
        public string TerTypeLgth { get => terTypeLgth; set => terTypeLgth = value; }
        public string TerTypeValue { get => terTypeValue; set => terTypeValue = value; }
    }
    struct iccApplication
    {
        private string rectype;
        private string entryNumber;
        private string primaryAIDLength;
        private string primaryAIDValue;
        private string defaultAppLabelLength;
        private string defaultAppValue;
        private string primaryAIDICCAppType;
        private string primaryAIDLowestAppVersion;
        private string primaryAIDHighestAppVersion;
        private string primaryAIDActionCode;
        private string numberOfDataObjectTReq;
        private string dataObjectForTReq;
        private string numberOfDataObjectCompletion;
        private string dataObjectForCompletion;
        private string numberOfSecondaryAID;
        private string secondaryAIDLgthValue;
        private string appSelectionIndicator;
        private string trk2DataForCentral;
        private string trk2DataUsedDuringICCTransaction;
        private string additionalTrk2DataLength;
        private string additionalTrk2Data;

        public string Rectype { get => rectype; set => rectype = value; }
        public string EntryNumber { get => entryNumber; set => entryNumber = value; }
        public string PrimaryAIDLength { get => primaryAIDLength; set => primaryAIDLength = value; }
        public string PrimaryAIDValue { get => primaryAIDValue; set => primaryAIDValue = value; }
        public string DefaultAppLabelLength { get => defaultAppLabelLength; set => defaultAppLabelLength = value; }
        public string DefaultAppValue { get => defaultAppValue; set => defaultAppValue = value; }
        public string PrimaryAIDICCAppType { get => primaryAIDICCAppType; set => primaryAIDICCAppType = value; }
        public string PrimaryAIDLowestAppVersion { get => primaryAIDLowestAppVersion; set => primaryAIDLowestAppVersion = value; }
        public string PrimaryAIDHighestAppVersion { get => primaryAIDHighestAppVersion; set => primaryAIDHighestAppVersion = value; }
        public string PrimaryAIDActionCode { get => primaryAIDActionCode; set => primaryAIDActionCode = value; }
        public string NumberOfDataObjectTReq { get => numberOfDataObjectTReq; set => numberOfDataObjectTReq = value; }
        public string DataObjectForTReq { get => dataObjectForTReq; set => dataObjectForTReq = value; }
        public string NumberOfDataObjectCompletion { get => numberOfDataObjectCompletion; set => numberOfDataObjectCompletion = value; }
        public string DataObjectForCompletion { get => dataObjectForCompletion; set => dataObjectForCompletion = value; }
        public string NumberOfSecondaryAID { get => numberOfSecondaryAID; set => numberOfSecondaryAID = value; }
        public string SecondaryAIDLgthValue { get => secondaryAIDLgthValue; set => secondaryAIDLgthValue = value; }
        public string AppSelectionIndicator { get => appSelectionIndicator; set => appSelectionIndicator = value; }
        public string Trk2DataForCentral { get => trk2DataForCentral; set => trk2DataForCentral = value; }
        public string Trk2DataUsedDuringICCTransaction { get => trk2DataUsedDuringICCTransaction; set => trk2DataUsedDuringICCTransaction = value; }
        public string AdditionalTrk2DataLength { get => additionalTrk2DataLength; set => additionalTrk2DataLength = value; }
        public string AdditionalTrk2Data { get => additionalTrk2Data; set => additionalTrk2Data = value; }
    };
    struct emvConfiguration
    {
        private string rectype;
        private string responseFlag;
        private string luno;
        private string msgSubclass;
        private string numberOfEntries;
        private List<iccCurrency> iccCurrencyDOTList;
        private List<iccTransaction> iccTransactionDOTList;
        private string configurationData;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string ResponseFlag { get => responseFlag; set => responseFlag = value; }
        public string Luno { get => luno; set => luno = value; }
        public string MsgSubclass { get => msgSubclass; set => msgSubclass = value; }
        public string NumberOfEntries { get => numberOfEntries; set => numberOfEntries = value; }
        public List<iccCurrency> IccCurrencyDOTList { get => iccCurrencyDOTList; set => iccCurrencyDOTList = value; }
        public List<iccTransaction> IccTransactionDOTList { get => iccTransactionDOTList; set => iccTransactionDOTList = value; }
        public string ConfigurationData { get => configurationData; set => configurationData = value; }
        public string Mac { get => mac; set => mac = value; }
    };
    
    class EMVConfiguration : IMessage
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        // System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //public readonly string emvTags = @",42,50,57,61,70,71,72,73,77,80,81,82,83,84,86,87,88,89,90,91,92,93,94," +
        //                        "95,97,98,99,4F,5A,5F20,5F24,5F25,5F28,5F2A,5F2D,5F30,5F34,5F36,5F50,5F53,5F54,5F55," +
        //                        "5F56,6F,8A,8C,8D,8E,8F,9A,9B,9C,9D,9F01,9F02,9F03,9F04,9F05,9F06,9F07,9F08,9F09,9F0B,9F0D," +
        //                        "9F0E,9F0F,9F10,9F11,9F12,9F13,9F14,9F15,9F16,9F17,9F18,9F1A,9F1B,9F1C,9F1D,9F1E,9F1F," +
        //                        "9F20,9F21,9F22,9F23,9F26,9F27,9F2D,9F2E,9F2F,9F32,9F33,9F34,9F35,9F36,9F37,9F38,9F39," +
        //                        "9F3A,9F3B,9F3C,9F3D,9F40,9F41,9F42,9F43,9F44,9F45,9F46,9F47,9F48,9F49,9F4A,9F4B,9F4C,9F4D," +
        //                        "9F4E,9F4F,A5,BF0C,";

        public virtual DataTable getDescription()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = '8' and subRecType like '0%'";

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

            return dts;

        }

        public virtual bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                emvConfiguration emv = parseData(r.typeContent);
                if (emv.NumberOfEntries == "") { emv.NumberOfEntries = r.typeAddData; }

                string sql = @"INSERT INTO EMVConfiguration([logkey],[rectype],[responseFlag],
	                        [luno],[msgSubclass],[numberOfEntries],[configurationData],[mac],[prjkey],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + emv.Rectype + "','" + emv.ResponseFlag + "','" +
                               emv.Luno + "','" + emv.MsgSubclass + "','" + emv.NumberOfEntries + "','" +
                               emv.ConfigurationData + "','" + emv.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public emvConfiguration parseData(string r)
        {
            emvConfiguration emv = new emvConfiguration();

            string[] tmpTypes = r.Split((char)0x1c);

            emv.Rectype = "8";
            emv.ResponseFlag = "";
            emv.Luno = tmpTypes[1];
            emv.MsgSubclass = tmpTypes[2];
            emv.NumberOfEntries = "";
            if (emv.MsgSubclass != "4" && emv.MsgSubclass != "5")
            {
                emv.NumberOfEntries = tmpTypes[3].Substring(0, 2);
            }
            emv.ConfigurationData = tmpTypes[3];

            if (tmpTypes.Length > 4)
                emv.Mac = tmpTypes[4];

            return emv;

        }

        public virtual string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = new List<DataTable>();
            dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable emvRecDt = getDescription();

                if (dts[0].Rows.Count > 0)
                {
                    for (int rowNum = 0; rowNum < dts[0].Rows.Count; rowNum++)
                    {
                            // message class
                            txtField += emvRecDt.Rows[0][3].ToString().Trim() + " = ";
                            txtField += dts[0].Rows[rowNum][2].ToString().Trim() + System.Environment.NewLine;

                            // response flag
                            txtField += emvRecDt.Rows[1][3].ToString().Trim() + " = ";
                            txtField += dts[0].Rows[rowNum][3].ToString().Trim() + System.Environment.NewLine;

                            // luno
                            txtField += emvRecDt.Rows[2][3].ToString().Trim() + " = ";
                            txtField += dts[0].Rows[rowNum][4].ToString().Trim() + System.Environment.NewLine;

                            // message subclass
                            txtField += emvRecDt.Rows[3][3].ToString().Trim() + " = ";
                            txtField += dts[0].Rows[rowNum][5].ToString().Trim() + System.Environment.NewLine;

                            // configuration data
                            txtField += emvRecDt.Rows[4][3].ToString().Trim() + " = ";
                            txtField += dts[0].Rows[rowNum][7].ToString().Trim() + System.Environment.NewLine;

                            // MAC
                            txtField += emvRecDt.Rows[5][3].ToString().Trim() + " = ";
                            txtField += dts[0].Rows[rowNum][8].ToString().Trim() + System.Environment.NewLine;
                    }
            }

            return txtField;
        }

    }
}
