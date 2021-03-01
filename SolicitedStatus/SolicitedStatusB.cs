using System.Collections.Generic;

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
        private string smartCardDataID;
        private string centralRequestedICCDO;
        private string rsltOfIssuerScriptProcessing;
        private string seqnumOfScriptCommand;
        private string scriptID;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string LastTranTSN { get => lastTranTSN; set => lastTranTSN = value; }
        public string DataId { get => dataId; set => dataId = value; }
        public string TransactionData { get => transactionData; set => transactionData = value; }
        public string Mac { get => mac; set => mac = value; }
        public string SmartCardDataID { get => smartCardDataID; set => smartCardDataID = value; }
        public string CentralRequestedICCDO { get => centralRequestedICCDO; set => centralRequestedICCDO = value; }
        public string RsltOfIssuerScriptProcessing { get => rsltOfIssuerScriptProcessing; set => rsltOfIssuerScriptProcessing = value; }
        public string SeqnumOfScriptCommand { get => seqnumOfScriptCommand; set => seqnumOfScriptCommand = value; }
        public string ScriptID { get => scriptID; set => scriptID = value; }
    };

    struct CassettesData
    {
        private string cassetteType;

    };
    struct CashDepositRecyclerData
    {
        private string numberOfRecyclerCsctTypesReported;
        private CassettesData cassetesDetail;
    };

    class SolicitedStatusB : SolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                solicitedStaB ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusB([logkey],[rectype],
	                        [luno],[timeVariant],[statusDescriptor],[lastTranTSN],
                            [dataId],[transactionData],[smartCardDataID],
	                        [centralRequestedICCDO],[rsltOfIssuerScriptProcessing],
	                        [seqnumOfScriptCommand],[scriptID],[mac],[prjkey],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" +
                               ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                               ss.LastTranTSN + "','" + ss.DataId + "','" + ss.TransactionData + "','" +
                               ss.SmartCardDataID + "','" + ss.CentralRequestedICCDO + "','" + ss.RsltOfIssuerScriptProcessing + "','" +
                               ss.SeqnumOfScriptCommand + "','" + ss.ScriptID + "','" +
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
            Digester digester = LoggerFactory.Create_Digester();

            string[] tmpTypes = r.Split((char)0x1c);

            ss.Rectype = "N";
            ss.Luno = tmpTypes[1];
            int i = 3;

            string[] tmp = tmpTypes[3].Split((char)0x1d);

            if (tmp[0].Length != 1)
            {
                i = 4;
                ss.TimeVariant = tmpTypes[3];
            }

            tmp = tmpTypes[i].Split((char)0x1d);

            if (tmp.Length > 1)
            {
                // CAM is included with Status Descriptor
                ss.StatusDescriptor = tmp[0];
                ss.SmartCardDataID = tmp[1];
                ss.CentralRequestedICCDO = digester.iccTLVTags(tmp[2]);
                if (tmp.Length > 3)
                {
                    ss.RsltOfIssuerScriptProcessing = tmp[3].Substring(0, 1);
                    ss.SeqnumOfScriptCommand = tmp[3].Substring(1, 1);
                    ss.ScriptID = digester.iccTLVTags(tmp[3].Substring(2, tmp[3].Length - 2));
                }
            }
            else
            {
                ss.StatusDescriptor = tmpTypes[i];
            }

            if (tmpTypes.Length > i + 1)
            {
                string[] statusInfo = tmpTypes[i + 1].Split((char)0x1d);
                ss.LastTranTSN = statusInfo[0];

                int x = 1;

                if (statusInfo.Length > x)
                {
                    if (statusInfo[x].Substring(0, 1) == "1" ||
                        statusInfo[x].Substring(0, 1) == "2")
                    {
                        ss.DataId = statusInfo[x].Substring(0, 1);
                        ss.TransactionData = statusInfo[x].Substring(1, statusInfo[x].Length - 1);
                        x++;
                    }
                }

                if (statusInfo.Length > x)
                {
                    ss.SmartCardDataID = statusInfo[x];
                    ss.CentralRequestedICCDO = digester.iccTLVTags(statusInfo[x + 1]);
                    if (statusInfo.Length > x + 2)
                    {
                        ss.RsltOfIssuerScriptProcessing = statusInfo[x + 2].Substring(0, 1);
                        ss.SeqnumOfScriptCommand = statusInfo[x + 2].Substring(1, 1);
                        ss.ScriptID = digester.iccTLVTags(statusInfo[x + 2].Substring(2, statusInfo[x + 2].Length - 2));
                    }
                }
            }

            if (tmpTypes.Length > i + 2)
                ss.Mac = tmpTypes[i + 2];

            return ss;
        }
    }
}
