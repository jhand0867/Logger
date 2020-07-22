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
        public string currencyType;
        public string responseFormat;
        public string responseLength;
        public string trCurrencyCodeTag;
        public string trCurrencyCodeLgth;
        public string trCurrencyCodeValue;
        public string trCurrencyExpTag;
        public string trCurrencyExpLgth;
        public string trCurrencyExpValue;
    }

    struct iccTransaction
    {

    };

    struct emvConfiguration
    {
        public string rectype;
        public string responseFlag;
        public string luno;
        public string msgSubclass;
        public string numberOfEntries;
        public List<iccCurrency> iccCurrencyDOTList;
        public List<iccTransaction> iccTransactionDOTList;
        public string configurationData;
        public string mac;
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
