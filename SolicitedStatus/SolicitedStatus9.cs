using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct solicitedSta9
    {
        private string rectype;
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
    };

    class SolicitedStatus9 : EMVConfiguration, IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'N'  AND subRecType like '9%'";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT TOP 1 * FROM solicitedStatus WHERE prjkey = '" + projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" +
                                                               logKey + "%'";
            DataTable dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable ss9 = getDescription();

            // txtField = ss9.Rows[0][3].ToString().Trim() + " = " + dts[0].Rows[0][3].ToString();

            if (dts[0].Rows.Count > 0)
            {
                for (int colNum = 3; colNum < dts[0].Columns.Count - 2; colNum++)
                {
                    if (dts[0].Rows[0][colNum].ToString() != "" &&
                        dts[0].Rows[0][colNum].ToString() != " ")
                    {
                        txtField += getOptionDescription(ss9, "9" + colNum.ToString("00"));
                        txtField += " = " + dts[0].Rows[0][colNum].ToString();
                        txtField += "\t" + System.Environment.NewLine;
                    }
                }
            }

            return txtField;
        }

        public virtual bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                solicitedSta9 ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatus([logkey],[rectype],
	                        [luno],[timeVariant],[statusDescriptor],[statusInformation],[smartCardDataID],
	                        [centralRequestedICCDO],[rsltOfIssuerScriptProcessing],[seqnumOfScriptCommand],
                        	[scriptID],[mac],[prjkey],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" +
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
                ss.CentralRequestedICCDO = iccTLVTags(tmp[2]);
                if (tmp.Length > 3)
                {
                    ss.RsltOfIssuerScriptProcessing = tmp[3].Substring(0, 1);
                    ss.SeqnumOfScriptCommand = tmp[3].Substring(1, 1);
                    ss.ScriptID = iccTLVTags(tmp[3].Substring(2, tmp[3].Length - 2));
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

        internal string getOptionDescription(DataTable dataTable, string field)
        {
            string optionDesc = "";
            // what's the description of the field
            foreach (DataRow item in dataTable.Rows)
            {
                if (item[2].ToString().Trim() == field)
                {
                    optionDesc = item[3].ToString().Trim();
                    break;
                }
            }
            return optionDesc;
        }
    }
}
