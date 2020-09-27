using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct solicitedSta8
    {
        private string rectype;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string digId;
        private string transactionStatus;
        private string errorSeverity;
        private string diagnosticStatus;
        private string suppliesStatus;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string DigId { get => digId; set => digId = value; }
        public string TransactionStatus { get => transactionStatus; set => transactionStatus = value; }
        public string ErrorSeverity { get => errorSeverity; set => errorSeverity = value; }
        public string DiagnosticStatus { get => diagnosticStatus; set => diagnosticStatus = value; }
        public string SuppliesStatus { get => suppliesStatus; set => suppliesStatus = value; }
        public string Mac { get => mac; set => mac = value; }
    };

    class SolicitedStatus8 : IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public DataTable getDescription()
        {
            throw new NotImplementedException();
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            throw new NotImplementedException();
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            return null;
        }

        public virtual bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                solicitedSta8 ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatus8([logkey],[rectype],[luno],
	                        [timeVariant],[statusDescriptor],[DIGId],[transactionStatus],[errorSeverity],
	                        [diagnosticStatus],[suppliesStatus],[mac],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" +
                               ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" + ss.DigId + "','" +
                               ss.TransactionStatus + "','" + ss.ErrorSeverity + "','" + ss.DiagnosticStatus + "','" +
                               ss.SuppliesStatus + "','" + ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedSta8 parseData(string r)
        {
            solicitedSta8 ss = new solicitedSta8();

            string[] tmpTypes = r.Split((char)0x1c);

            ss.Rectype = "N";
            ss.Luno = tmpTypes[1];
            int i = 3;

            if (tmpTypes[3].Length != 1)
            {
                i = 4;
                ss.TimeVariant = tmpTypes[3];
            }

            ss.StatusDescriptor = tmpTypes[i];

            ss.DigId = tmpTypes[i + 1].Substring(0,1);
            ss.TransactionStatus = tmpTypes[i + 1].Substring(1, tmpTypes[i+1].Length-1);
            
            if (tmpTypes.Length > i + 2)
            {
                ss.ErrorSeverity = tmpTypes[i + 2];
            }

            if (tmpTypes.Length > i + 3)
            {
                ss.DiagnosticStatus = tmpTypes[i + 3];
            }

            if (tmpTypes.Length > i + 4)
            {
                ss.SuppliesStatus = tmpTypes[i + 4];
            }

            if (tmpTypes.Length > i + 5)
                ss.Mac = tmpTypes[i + 5];

            return ss;
        }
    }
}

