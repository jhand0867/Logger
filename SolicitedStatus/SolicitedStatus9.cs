using System.Collections.Generic;

namespace Logger
{

    struct solicitedSta9
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string statusInformation;
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
        public string StatusInformation { get => statusInformation; set => statusInformation = value; }
        public string SmartCardDataID { get => smartCardDataID; set => smartCardDataID = value; }
        public string CentralRequestedICCDO { get => centralRequestedICCDO; set => centralRequestedICCDO = value; }
        public string RsltOfIssuerScriptProcessing { get => rsltOfIssuerScriptProcessing; set => rsltOfIssuerScriptProcessing = value; }
        public string SeqnumOfScriptCommand { get => seqnumOfScriptCommand; set => seqnumOfScriptCommand = value; }
        public string ScriptID { get => scriptID; set => scriptID = value; }
        public string Mac { get => mac; set => mac = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    };

    class SolicitedStatus9 : SolicitedStatus //, IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                solicitedSta9 ss = parseData(r.typeContent);

                if (ss.Luno == "") ss.Luno = null;


                string sql = @"INSERT INTO solicitedStatus9([logkey],[rectype],[messageClass],[messageSubClass],
	                        [luno],[timeVariant],[statusDescriptor],[statusInformation],[smartCardDataID],
	                        [centralRequestedICCDO],[rsltOfIssuerScriptProcessing],[seqnumOfScriptCommand],
                        	[scriptID],[mac],[prjkey],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" + ss.MessageClass + "','" + ss.MessageSubClass + "','" +
                               ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                               ss.StatusInformation + "','" + ss.SmartCardDataID + "','" + ss.CentralRequestedICCDO + "','" +
                               ss.RsltOfIssuerScriptProcessing + "','" + ss.SeqnumOfScriptCommand + "','" +
                               ss.ScriptID + "','" + ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedSta9 parseData(string r)
        {
            solicitedSta9 ss = new solicitedSta9();
            Digester digester = LoggerFactory.Create_Digester();

            string[] tmpTypes = r.Split((char)0x1c);

            ss.Rectype = "N";
            ss.MessageClass = tmpTypes[0].Substring(10, 1);
            ss.MessageSubClass = tmpTypes[0].Substring(11, 1);
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

            if (tmpTypes.Length > i + 2)
                ss.Mac = tmpTypes[i + 2];

            return ss;
        }
    }
}
