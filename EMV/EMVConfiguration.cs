﻿using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct iccCurrency
    {
        private string rectype;
        private string currencyType;
        private string responseFormat2Tag;
        private string responseFormat2Length;
        private string responseFormat2Value;

        public string Rectype { get => rectype; set => rectype = value; }
        public string CurrencyType { get => currencyType; set => currencyType = value; }
        public string ResponseFormat2Tag { get => responseFormat2Tag; set => responseFormat2Tag = value; }
        public string ResponseFormat2Length { get => responseFormat2Length; set => responseFormat2Length = value; }
        public string ResponseFormat2Value { get => responseFormat2Value; set => responseFormat2Value = value; }
    }
    struct iccTransaction
    {
        private string rectype;
        private string transactionType;
        private string responseFormat2Tag;
        private string responseFormat2Length;
        private string responseFormat2Value;

        public string Rectype { get => rectype; set => rectype = value; }
        public string TransactionType { get => transactionType; set => transactionType = value; }
        public string ResponseFormat2Tag { get => responseFormat2Tag; set => responseFormat2Tag = value; }
        public string ResponseFormat2Length { get => responseFormat2Length; set => responseFormat2Length = value; }
        public string ResponseFormat2Value { get => responseFormat2Value; set => responseFormat2Value = value; }
    }
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
        private string responseFormat2Tag;
        private string responseFormat2Length;
        private string responseFormat2Value;

        public string Rectype { get => rectype; set => rectype = value; }
        public string ResponseFormat2Tag { get => responseFormat2Tag; set => responseFormat2Tag = value; }
        public string ResponseFormat2Length { get => responseFormat2Length; set => responseFormat2Length = value; }
        public string ResponseFormat2Value { get => responseFormat2Value; set => responseFormat2Value = value; }
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

    class EMVConfiguration : App, IMessage
    {
        public virtual DataTable getDescription()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = '8' and subRecType like '0%'";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * from EMVConfiguration WHERE logID = '" + logID + "' AND prjkey = '" + projectKey + "' AND logkey LIKE '" + logKey + "%' LIMIT 1";
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
            if (tmpTypes[0].Length > 11)
            {
                emv.ResponseFlag = tmpTypes[0].Substring(tmpTypes[0].Length - 1, 1);
            }
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
                for (int colNum = 2; colNum < dts[0].Columns.Count - 3; colNum++)
                    txtField += App.Prj.getOptionDescription(emvRecDt, colNum.ToString("00"), dts[0].Rows[0][colNum].ToString());

            return txtField;
        }

    }
}
