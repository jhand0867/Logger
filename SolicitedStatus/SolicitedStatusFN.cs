using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct solicitedStaFN
    {
        private string rectype;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string messageIdentifier;
	    private string acceptedCashItemsId;
	    private string cashTypeData;
	    private string ecb6NoteRetationModeId;
	    private string ecb6NoteRetantionMode;
	    private string dataId;
	    private string data;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
        public string AcceptedCashItemsId { get => acceptedCashItemsId; set => acceptedCashItemsId = value; }
        public string CashTypeData { get => cashTypeData; set => cashTypeData = value; }
        public string Ecb6NoteRetationModeId { get => ecb6NoteRetationModeId; set => ecb6NoteRetationModeId = value; }
        public string Ecb6NoteRetantionMode { get => ecb6NoteRetantionMode; set => ecb6NoteRetantionMode = value; }
        public string DataId { get => dataId; set => dataId = value; }
        public string Data { get => data; set => data = value; }
        public string Mac { get => mac; set => mac = value; }
    };

    class SolicitedStatusFN : SolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                solicitedStaFN ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusFN([logkey],[rectype],
	                        [luno],[timeVariant],[statusDescriptor],[messageIdentifier],[AcceptedCashItemsId],
	                        [cashTypeData],[ecb6NoteRetationModeId],[ecb6NoteRetantionMode],[dataId],
	                        [data],[mac],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" + ss.Luno + "','" +
                            ss.TimeVariant + "','" + ss.StatusDescriptor + "','" + ss.MessageIdentifier + "','" +
                            ss.AcceptedCashItemsId + "','" + ss.CashTypeData + "','" + ss.Ecb6NoteRetationModeId + "','" +
                            ss.Ecb6NoteRetantionMode + "','" + ss.DataId + "','" + ss.Data + "','" +
                            ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedStaFN parseData(string r)
        {
            solicitedStaFN ss = new solicitedStaFN();

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
            ss.AcceptedCashItemsId = tmpTypes[i + 1].Substring(1, 1);
            if (tmpTypes[i+1].Length > 2) ss.CashTypeData = tmpTypes[i + 1].Substring(2, tmpTypes[i + 1].Length - 2);

            if (tmpTypes.Length > i+2)
            {
                ss.Ecb6NoteRetationModeId = tmpTypes[i + 2].Substring(0, 1);
                ss.Ecb6NoteRetationModeId = tmpTypes[i + 2].Substring(1, 3);
            }

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
