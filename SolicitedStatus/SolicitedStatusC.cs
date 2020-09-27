using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct solicitedStaC
    {
        private string rectype;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string statusValue;
        private string statusQualifier;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string StatusValue { get => statusValue; set => statusValue = value; }
        public string StatusQualifier { get => statusQualifier; set => statusQualifier = value; }
        public string Mac { get => mac; set => mac = value; }
    };

    class SolicitedStatusC : IMessage
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
                solicitedStaC ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusC([logkey],[rectype],
	                        [luno],[timeVariant],[statusDescriptor],[statusValue],[statusQualifier],[mac],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" +
                               ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                               ss.StatusValue + "','" + ss.StatusQualifier + "','" + ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedStaC parseData(string r)
        {
            solicitedStaC ss = new solicitedStaC();

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
            ss.StatusValue = tmpTypes[i+1].Substring(0,1);

            if (tmpTypes[i+1].Length > 1 ) ss.StatusQualifier = tmpTypes[i+1].Substring(1, 2);

            if (tmpTypes.Length > i+2)
                ss.Mac = tmpTypes[i+2];

            return ss;
        }
    }
}
