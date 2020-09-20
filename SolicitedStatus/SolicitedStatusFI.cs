﻿using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct solicitedStaFI
    {
        private string rectype;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string messageIdentifier;
        private string suppliesStatusID;
        private string suppliesStatusData;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
        public string SuppliesStatusID { get => suppliesStatusID; set => suppliesStatusID = value; }
        public string SuppliesStatusData { get => suppliesStatusData; set => suppliesStatusData = value; }
        public string Mac { get => mac; set => mac = value; }
    };

    class SolicitedStatusFI : EMVConfiguration, IMessage
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
                solicitedStaFI ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusFI([logkey],[rectype],
	                        [luno],[timeVariant],[statusDescriptor],[messageIdentifier],[suppliesStatusID],
	                        [suppliesStatusData],[mac],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" +
                            ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                            ss.MessageIdentifier + "','" + ss.SuppliesStatusID + "','" + ss.SuppliesStatusData + "','" +
                            ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedStaFI parseData(string r)
        {
            solicitedStaFI ss = new solicitedStaFI();

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
            // separating data by device
            string[] statusInfo = tmpTypes[i + 1].Split((char)0x1d);

            ss.MessageIdentifier = statusInfo[0].Substring(0, 1);

            // this is the initial element id A
            ss.SuppliesStatusID = statusInfo[0].Substring(1, 1);

            ss.SuppliesStatusData = statusInfo[0].Substring(2, statusInfo[0].Length - 2);
            for (int x = 1; x < statusInfo.Length; x++)
            {
                ss.SuppliesStatusData += "," + statusInfo[x];
            }

            if (tmpTypes.Length > i + 2)
                ss.Mac = tmpTypes[i + 2];
            return ss;
        }
    }
}
