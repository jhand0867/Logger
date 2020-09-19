using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct solicitedStaF1
    {
        private string rectype;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string messageIdentifier;
        private string configurationId;
        private string hardwareFitness;
        private string hardwareConfig;
        private string supplyStatus;
        private string sensorStatus;
        private string releaseNumber;
        private string softwareId;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
        public string ConfigurationId { get => configurationId; set => configurationId = value; }
        public string HardwareFitness { get => hardwareFitness; set => hardwareFitness = value; }
        public string HardwareConfig { get => hardwareConfig; set => hardwareConfig = value; }
        public string SupplyStatus { get => supplyStatus; set => supplyStatus = value; }
        public string SensorStatus { get => sensorStatus; set => sensorStatus = value; }
        public string ReleaseNumber { get => releaseNumber; set => releaseNumber = value; }
        public string SoftwareId { get => softwareId; set => softwareId = value; }
        public string Mac { get => mac; set => mac = value; }
    };

    class SolicitedStatusF1 : EMVConfiguration, IMessage
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
                solicitedStaF1 ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusF1([logkey],[rectype],
	                        [luno],[timeVariant],[statusDescriptor],[messageIdentifier],[configurationId],
	                        [hardwareFitness],[hardwareConfig],[supplyStatus],[sensorStatus],[releaseNumber],
	                        [softwareId],[mac],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" +
                            ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                            ss.MessageIdentifier + "','" + ss.ConfigurationId + "','" + ss.HardwareFitness + "','" +
                            ss.HardwareConfig + "','" + ss.SupplyStatus + "','" + ss.SensorStatus + "','" +
                            ss.ReleaseNumber + "','" + ss.SoftwareId + "','" + ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedStaF1 parseData(string r)
        {
            solicitedStaF1 ss = new solicitedStaF1();

            string[] tmpTypes = r.Split((char)0x1c);

            ss.Rectype = "N";
            ss.Luno = tmpTypes[1];
            // ss.TimeVariant = tmpTypes[2];
            int i = 3;
            if (tmpTypes[3].Length == 8)
            {
                i = 4;
                ss.TimeVariant = tmpTypes[3];
            }

            ss.StatusDescriptor = tmpTypes[i];

            ss.MessageIdentifier = tmpTypes[i+1].Substring(0, 1);
            ss.ConfigurationId = tmpTypes[i+1].Substring(1, 4);
            ss.HardwareFitness = tmpTypes[i+2];
            ss.HardwareConfig = tmpTypes[i+3];
            ss.SupplyStatus = tmpTypes[i+4];
            ss.SensorStatus = tmpTypes[i+5];

            if (tmpTypes.Length > i+6)
                 ss.ReleaseNumber = tmpTypes[i+6];
            if (tmpTypes.Length > i+7)
                ss.SoftwareId = tmpTypes[i+7];
            if (tmpTypes.Length > i+8)
                ss.Mac = tmpTypes[i+8];

            return ss;
        }
    }
}

