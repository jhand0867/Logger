using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct solicitedStaB
    {
        private string rectype;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string lastTranTSN;
        private string dataId;
        private string transactionData;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string LastTranTSN { get => lastTranTSN; set => lastTranTSN = value; }
        public string DataId { get => dataId; set => dataId = value; }
        public string TransactionData { get => transactionData; set => transactionData = value; }
        public string Mac { get => mac; set => mac = value; }
    };

    struct CassettesData {
        private string cassetteType;

    };
    struct CashDepositRecyclerData
    {
        private string numberOfRecyclerCsctTypesReported;
        private CassettesData cassetesDetail;
    };

    class SolicitedStatusB : EMVConfiguration, IMessage
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
                solicitedStaB ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusB([logkey],[rectype],
	                        [luno],[timeVariant],[statusDescriptor],[lastTranTSN],
                            [dataId],[transactionData],[mac],[prjkey],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" +
                               ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                               ss.LastTranTSN + "','" + ss.DataId + "','" + ss.TransactionData + "','" + 
                               ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedStaB parseData(string r)
        {
            solicitedStaB ss = new solicitedStaB();

            string[] tmpTypes = r.Split((char)0x1c);

            ss.Rectype = "N";
            ss.Luno = tmpTypes[1];
            // ss.TimeVariant = tmpTypes[2];
            ss.StatusDescriptor = tmpTypes[3];
            
            string[] statusInfo = tmpTypes[4].Split((char)0x1d);

            ss.LastTranTSN = statusInfo[0];

            if (statusInfo.Length > 1)
            {
                ss.DataId = statusInfo[1];
                if (ss.DataId == "CAM")
                {
                    ss.TransactionData = iccTLVTags(statusInfo[2]);
                }
                else
                {
                    ss.TransactionData = statusInfo[2];
                }
            }

             if (tmpTypes.Length > 5)
                    ss.Mac = tmpTypes[5];

            return ss;
        }
    }
}
