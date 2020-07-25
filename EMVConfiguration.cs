using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logger
{
    struct iccCurrency
    {

        private string currencyType;
        private string responseFormat;
        private string responseLength;
        private string trCurrencyCodeTag;
        private string trCurrencyCodeLgth;
        private string trCurrencyCodeValue;
        private string trCurrencyExpTag;
        private string trCurrencyExpLgth;
        private string trCurrencyExpValue;

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
        private string transactionType;
        private string responseFormat;
        private string responseLength;
        private string transactionTypeTag;
        private string transactionTypeLgth;
        private string transactionTypeValue;
        private string transactionCatCodeTag;
        private string transactionCatCodeLgth;
        private string transactionCatCodeValue;

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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
         System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // TODO: getDescription, getRecord
        public DataTable getDescription()
        {
            throw new NotImplementedException();
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            throw new NotImplementedException();
        }

        public bool writeData(List<typeRec> typeRecs, string key, string logID)
        {
            throw new NotImplementedException();
        }

        public bool writeData(typeRec r, string Key, string logID)
        {
            log.Info("Adding to Database ");
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                string[] tmpTypes = r.typeContent.Split((char)0x1c); ;

                emvConfiguration emv = new emvConfiguration();
                emv.Rectype = "8";
                emv.ResponseFlag = "";
                emv.Luno = tmpTypes[1];
                emv.MsgSubclass = tmpTypes[2];
                emv.NumberOfEntries = tmpTypes[3].Substring(0, 2);
                emv.ConfigurationData = tmpTypes[3];
                emv.Mac = tmpTypes[4];

                string sql = @"INSERT INTO EMVConfiguration([logkey],[rectype],[responseFlag],
	                        [luno],[msgSubclass],[numberOfEntries],[configurationData],[mac],[prjkey],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + emv.Rectype + "','" + emv.ResponseFlag + "','" +
                               emv.Luno + "','" + emv.MsgSubclass + "','" + emv.NumberOfEntries + "','" +
                               emv.ConfigurationData + "','" + emv.Mac + "','" + Key + "'," + logID + ")";

                log.Debug("Adding record " + sql);
                command = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand.ExecuteNonQuery();
                command.Dispose();
                log.Debug("Record Added");

                cnn.Close();
                return true;
            }

            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return false;

            }

        }
    }
}
