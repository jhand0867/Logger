﻿using System.Collections.Generic;

namespace Logger
{

    struct encryptorInitD
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
        private string luno;
        private string informationIdentifier;
        private string eppVendorCo;
        private string pciVendorName;
        private string eppModelId;
        private string pciModelName;
        private string eppHardwareId;
        private string pciHardwareName;
        private string eppFirmwareId;
        private string pciFirmwareName;
        private string eppAppIds;
        private string pciAppName;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string InformationIdentifier { get => informationIdentifier; set => informationIdentifier = value; }
        public string EppVendorCo { get => eppVendorCo; set => eppVendorCo = value; }
        public string PciVendorName { get => pciVendorName; set => pciVendorName = value; }
        public string EppModelId { get => eppModelId; set => eppModelId = value; }
        public string PciModelName { get => pciModelName; set => pciModelName = value; }
        public string EppHardwareId { get => eppHardwareId; set => eppHardwareId = value; }
        public string PciHardwareName { get => pciHardwareName; set => pciHardwareName = value; }
        public string EppFirmwareId { get => eppFirmwareId; set => eppFirmwareId = value; }
        public string PciFirmwareName { get => pciFirmwareName; set => pciFirmwareName = value; }
        public string EppAppIds { get => eppAppIds; set => eppAppIds = value; }
        public string PciAppName { get => pciAppName; set => pciAppName = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    };

    class EncryptorInitDataD : EncryptorInitData
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                encryptorInitD kD = parseData(r.typeContent);

                string sql = @"INSERT INTO encryptorInitDataD([logkey],[rectype],[messageClass],[messageSubClass],[luno],[informationIdentifier],
	                        [eppVendorCo],[pciVendorName],[eppModelId],[pciModelName],[eppHardwareId],
	                        [pciHardwareName],[eppFirmwareId],[pciFirmwareName],[eppAppIds],
	                        [pciAppName],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + kD.Rectype + "','" + kD.MessageClass + "','" + kD.MessageSubClass + "','" + kD.Luno + "','" +
                               kD.InformationIdentifier + "','" + kD.EppVendorCo + "','" + kD.PciVendorName + "','" +
                               kD.EppModelId + "','" + kD.PciModelName + "','" + kD.EppHardwareId + "','" +
                               kD.PciHardwareName + "','" + kD.EppFirmwareId + "','" + kD.PciFirmwareName + "','" +
                               kD.EppAppIds + "','" + kD.PciAppName + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public encryptorInitD parseData(string r)
        {
            encryptorInitD kD = new encryptorInitD();

            string[] tmpTypes = r.Split((char)0x1c);

            kD.Rectype = "K";
            kD.MessageClass = tmpTypes[0].Substring(10, 1);
            kD.MessageSubClass = tmpTypes[0].Substring(11, 1);
            kD.Luno = tmpTypes[1];
            kD.InformationIdentifier = tmpTypes[3];

            string[] eppAttributes = tmpTypes[4].Split((char)0x1d);

            if (eppAttributes.Length > 0)
            {
                if (eppAttributes[0].Length > 0) kD.EppVendorCo = eppAttributes[0].Substring(0, 1);
                if (eppAttributes[0].Length > 1) kD.PciVendorName = eppAttributes[0].Substring(1, eppAttributes[0].Length - 1);
            }
            if (eppAttributes.Length > 1)
            {
                if (eppAttributes[1].Length > 0) kD.EppModelId = eppAttributes[1].Substring(0, 1);
                if (eppAttributes[1].Length > 1) kD.PciModelName = eppAttributes[1].Substring(1, eppAttributes[1].Length - 1);
            }
            if (eppAttributes.Length > 2)
            {
                if (eppAttributes[2].Length > 0) kD.EppHardwareId = eppAttributes[2].Substring(0, 1);
                if (eppAttributes[2].Length > 1) kD.PciHardwareName = eppAttributes[2].Substring(1, eppAttributes[2].Length - 1);
            }
            if (eppAttributes.Length > 3)
            {
                if (eppAttributes[3].Length > 0) kD.EppFirmwareId = eppAttributes[3].Substring(0, 1);
                if (eppAttributes[3].Length > 1) kD.PciFirmwareName = eppAttributes[3].Substring(1, eppAttributes[3].Length - 1);
            }
            if (eppAttributes.Length > 4)
            {
                if (eppAttributes[4].Length > 0) kD.EppAppIds = eppAttributes[4].Substring(0, 1);
                if (eppAttributes[4].Length > 1) kD.PciAppName = eppAttributes[4].Substring(1, eppAttributes[4].Length - 1);
            }

            return kD;
        }
    }
}
