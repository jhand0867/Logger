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
        private string statusInformation;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string StatusInformation { get => statusInformation; set => statusInformation = value; }
        public string Mac { get => mac; set => mac = value; }
    };

    class SolicitedStatusB : App, IMessage
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
	                        [luno],[timeVariant],[statusDescriptor],[statusInformation],[mac],[prjkey],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" +
                               ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                               ss.StatusInformation + "','" + ss.Mac + "','" + Key + "'," + logID + ")";

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
            ss.TimeVariant = tmpTypes[2];
            ss.StatusDescriptor = tmpTypes[3];

                if (tmpTypes.Length > 4)
                    ss.StatusInformation = tmpTypes[4];

                if (tmpTypes.Length > 5)
                    ss.Mac = tmpTypes[5];

            return ss;
        }
    }
}
