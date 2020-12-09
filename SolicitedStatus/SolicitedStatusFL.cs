using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct solicitedStaFL
    {
        private string rectype;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string messageIdentifier;
        private string releaseNumberId;
        private string releaseNumber;
        private string softwareIdId;
        private string softwareId;
        private string dataId;
        private string data;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
        public string ReleaseNumberId { get => releaseNumberId; set => releaseNumberId = value; }
        public string ReleaseNumber { get => releaseNumber; set => releaseNumber = value; }
        public string SoftwareIdId { get => softwareIdId; set => softwareIdId = value; }
        public string SoftwareId { get => softwareId; set => softwareId = value; }
        public string DataId { get => dataId; set => dataId = value; }
        public string Data { get => data; set => data = value; }
        public string Mac { get => mac; set => mac = value; }
    };

    class SolicitedStatusFL : SolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                solicitedStaFL ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusFL([logkey],[rectype],
	                        [luno],[timeVariant],[statusDescriptor],[messageIdentifier],[releaseNumberId],[releaseNumber],
	                        [softwareIdId],[softwareId],[dataId],[data],[mac],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" +
                            ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                            ss.MessageIdentifier + "','" + ss.ReleaseNumberId + "','" + ss.ReleaseNumber + "','" +
                            ss.SoftwareIdId + "','" + ss.SoftwareId + "','" + ss.DataId + "','" +
                            ss.Data + "','" + ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedStaFL parseData(string r)
        {
            solicitedStaFL ss = new solicitedStaFL();

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
            ss.MessageIdentifier = tmpTypes[i + 1].Substring(0, 1);
            ss.ReleaseNumberId = tmpTypes[i + 1].Substring(1, 1);
            ss.ReleaseNumber = tmpTypes[i + 1].Substring(2, tmpTypes[i+1].Length - 2);

            ss.SoftwareIdId = tmpTypes[i + 2].Substring(0, 1);
            ss.SoftwareId = tmpTypes[i + 2].Substring(1, tmpTypes[i + 2].Length - 1);

            if (tmpTypes.Length > i + 3)
            {
                ss.DataId = tmpTypes[i + 3].Substring(0, 1);
                ss.Data = tmpTypes[i + 3].Substring(1, tmpTypes[i + 3].Length - 1);
            }

            if (tmpTypes.Length > i + 4)
                ss.Mac = tmpTypes[i + 4];

            return ss;
        }
    }
}
