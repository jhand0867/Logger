using System.Collections.Generic;

namespace Logger
{

    struct unsolicitedStaC
    {
        private string rectype;
        private string luno;
        private string dig;
        private string deviceStatus;
        private string errorSeverity;
        private string applicationType;
        private string applicationId;
        private string cla;
        private string ins;
        private string parameter1;
        private string parameter2;
        private string lengthCmd;
        private string cmdData;
        private string lengthExpected;
        private string responseData;
        private string sw1;
        private string sw2;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string Dig { get => dig; set => dig = value; }
        public string DeviceStatus { get => deviceStatus; set => deviceStatus = value; }
        public string ErrorSeverity { get => errorSeverity; set => errorSeverity = value; }
        public string ApplicationType { get => applicationType; set => applicationType = value; }
        public string ApplicationId { get => applicationId; set => applicationId = value; }
        public string Cla { get => cla; set => cla = value; }
        public string Ins { get => ins; set => ins = value; }
        public string Parameter1 { get => parameter1; set => parameter1 = value; }
        public string Parameter2 { get => parameter2; set => parameter2 = value; }
        public string LengthCmd { get => lengthCmd; set => lengthCmd = value; }
        public string CmdData { get => cmdData; set => cmdData = value; }
        public string LengthExpected { get => lengthExpected; set => lengthExpected = value; }
        public string ResponseData { get => responseData; set => responseData = value; }
        public string Sw1 { get => sw1; set => sw1 = value; }
        public string Sw2 { get => sw2; set => sw2 = value; }
    }

    class UnsolicitedStatusC : UnsolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                unsolicitedStaC us = parseData(r.typeContent);

                string sql = @"INSERT INTO unsolicitedStatusC([logkey],[rectype],[luno],
	                        [dig],[deviceStatus],[errorSeverity],[applicationType],[applicationId],
	                        [cla],[ins],[parameter1],[parameter2],[lengthCmd],[cmdData],
	                        [lengthExpected],[responseData],[sw1],[sw2],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + us.Rectype + "','" + us.Luno + "','" +
                               us.Dig + "','" + us.DeviceStatus + "','" + us.ErrorSeverity + "','" +
                               us.ApplicationType + "','" + us.ApplicationId + "','" + us.Cla + "','" + us.Ins + "','" +
                               us.Parameter1 + "','" + us.Parameter2 + "','" + us.LengthCmd + "','" + 
                               us.CmdData + "','" + us.LengthExpected + "','" + us.ResponseData + "','" + 
                               us.Sw1 + "','" + us.Sw2 + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public unsolicitedStaC parseData(string r)
        {
            unsolicitedStaC us = new unsolicitedStaC();

            string[] tmpTypes = r.Split((char)0x1c);

            us.Rectype = "U";
            us.Luno = tmpTypes[1];
            int i = 3;

            us.Dig = tmpTypes[i].Substring(0, 1);
            us.DeviceStatus = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);

            i++;

            if (tmpTypes.Length > i)
            {
                us.ErrorSeverity = tmpTypes[i];
            }

            i++;

            if (tmpTypes.Length > i)
            {
                int aidOffset = 3;
                us.ApplicationType = tmpTypes[i].Substring(0,3);
                us.ApplicationId = "";
                if (us.ApplicationType == "CAM")
                {
                    // Tag 9F06 -> tmpTypes[i].Substring(3, 4);
                    aidOffset = 6 + int.Parse(tmpTypes[i].Substring(7, 2)) * 2;
                    us.ApplicationId = tmpTypes[i].Substring(3, aidOffset);
                    aidOffset = aidOffset + 3;
                }
                us.Cla = tmpTypes[i].Substring(aidOffset, 2);
                aidOffset = aidOffset + 2;

                us.Ins = tmpTypes[i].Substring(aidOffset, 2);
                aidOffset = aidOffset + 2;

                us.Parameter1 = tmpTypes[i].Substring(aidOffset, 2);
                aidOffset = aidOffset + 2;

                us.Parameter2 = tmpTypes[i].Substring(aidOffset, 2);
                aidOffset = aidOffset + 2;

                us.LengthCmd = tmpTypes[i].Substring(aidOffset, 2);
                aidOffset = aidOffset + 2;

                int tmpOffset = int.Parse(us.LengthCmd) * 2;
                if (tmpOffset > 0)
                {
                    us.CmdData = tmpTypes[i].Substring(aidOffset, tmpOffset);
                    aidOffset = aidOffset + tmpOffset;
                }

                us.LengthExpected = tmpTypes[i].Substring(aidOffset, 2);
                aidOffset = aidOffset + 2;

                tmpOffset = int.Parse(us.LengthExpected) * 2;
                if (tmpOffset > 0)
                {
                    us.ResponseData = tmpTypes[i].Substring(aidOffset, tmpOffset);
                    aidOffset = aidOffset + tmpOffset;
                }
                us.Sw1 = tmpTypes[i].Substring(aidOffset, 2);
                aidOffset = aidOffset + 2;

                us.Sw2 = tmpTypes[i].Substring(aidOffset, 2);
            }

            return us;
        }
    }
}

