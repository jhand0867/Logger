using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
