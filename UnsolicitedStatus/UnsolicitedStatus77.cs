using System.Collections.Generic;

namespace Logger
{

    struct unsolicitedSta77
    {
        private string rectype;
        private string messageClass;
        private string messageSubclass;
        private string luno;
        private string dig;
        private string deviceStatus;
        private string errorCode;
        private string escrowCounts;
        private string vaultedCounts;
        private string returnCounts;
        private string notesReturnedExitSlot;
        private string notesInEscrow;
        private string justVaulted;
        private string escrowCountsNoteType;
        private string escrowCountsDecValue;
        private string voltedCountsNoteType;
        private string voltedCountsDecValue;
        private string returnCountsNoteType;
        private string returnCountsDecValue;
        private string notesReturnedExcessNinety;
        private string notesInEscrowExcessNinety;
        private string justVaultedExcessNinety;
        private string errorSeverity;
        private string diagnosticStatus;
        private string suppliesStatus;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string Dig { get => dig; set => dig = value; }
        public string DeviceStatus { get => deviceStatus; set => deviceStatus = value; }
        public string ErrorCode { get => errorCode; set => errorCode = value; }
        public string EscrowCounts { get => escrowCounts; set => escrowCounts = value; }
        public string VaultedCounts { get => vaultedCounts; set => vaultedCounts = value; }
        public string ReturnCounts { get => returnCounts; set => returnCounts = value; }
        public string NotesReturnedExitSlot { get => notesReturnedExitSlot; set => notesReturnedExitSlot = value; }
        public string NotesInEscrow { get => notesInEscrow; set => notesInEscrow = value; }
        public string JustVaulted { get => justVaulted; set => justVaulted = value; }
        public string EscrowCountsNoteType { get => escrowCountsNoteType; set => escrowCountsNoteType = value; }
        public string EscrowCountsDecValue { get => escrowCountsDecValue; set => escrowCountsDecValue = value; }
        public string VoltedCountsNoteType { get => voltedCountsNoteType; set => voltedCountsNoteType = value; }
        public string VoltedCountsDecValue { get => voltedCountsDecValue; set => voltedCountsDecValue = value; }
        public string ReturnCountsNoteType { get => returnCountsNoteType; set => returnCountsNoteType = value; }
        public string ReturnCountsDecValue { get => returnCountsDecValue; set => returnCountsDecValue = value; }
        public string NotesReturnedExcessNinety { get => notesReturnedExcessNinety; set => notesReturnedExcessNinety = value; }
        public string NotesInEscrowExcessNinety { get => notesInEscrowExcessNinety; set => notesInEscrowExcessNinety = value; }
        public string JustVaultedExcessNinety { get => justVaultedExcessNinety; set => justVaultedExcessNinety = value; }
        public string ErrorSeverity { get => errorSeverity; set => errorSeverity = value; }
        public string DiagnosticStatus { get => diagnosticStatus; set => diagnosticStatus = value; }
        public string SuppliesStatus { get => suppliesStatus; set => suppliesStatus = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubclass { get => messageSubclass; set => messageSubclass = value; }
    };

    class UnsolicitedStatus77 : UnsolicitedStatus
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                unsolicitedSta77 us = parseData(r.typeContent);

                string sql = @"INSERT INTO unsolicitedStatus77([logkey],[rectype],[messageClass],[messageSubclass],[luno],
	                        [dig],[deviceStatus],[errorCode],[escrowCounts],[vaultedCounts],[returnCounts],

	                        [notesReturnedExitSlot],[notesInEscrow],[justVaulted],[escrowCountsNoteType],
                            [escrowCountsDecValue],[voltedCountsNoteType],[voltedCountsDecValue],
	                        [returnCountsNoteType],[returnCountsDecValue],[notesReturnedExcessNinety],
	                        [notesInEscrowExcessNinety],[justVaultedExcessNinety],
                            [errorSeverity],[diagnosticStatus], [suppliesStatus],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + us.Rectype + "','" + us.MessageClass + "','" +
                               us.MessageSubclass + "','" + us.Luno + "','" +
                               us.Dig + "','" + us.DeviceStatus + "','" + us.ErrorCode + "','" + us.EscrowCounts + "','" +
                               us.VaultedCounts + "','" + us.ReturnCounts + "','" + us.NotesReturnedExitSlot + "','" +
                               us.NotesInEscrow + "','" + us.JustVaulted + "','" + us.EscrowCountsNoteType + "','" +
                               us.EscrowCountsDecValue + "','" + us.VoltedCountsNoteType + "','" +
                               us.VoltedCountsDecValue + "','" + us.ReturnCountsNoteType + "','" +
                               us.ReturnCountsDecValue + "','" + us.NotesReturnedExcessNinety + "','" +
                               us.NotesInEscrowExcessNinety + "','" + us.JustVaultedExcessNinety + "','" +
                               us.ErrorSeverity + "','" + us.DiagnosticStatus + "','" + us.SuppliesStatus + "','" +
                               Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public unsolicitedSta77 parseData(string r)
        {
            unsolicitedSta77 us = new unsolicitedSta77();

            string[] tmpTypes = r.Split((char)0x1c);

            us.Rectype = "U";
            us.MessageClass = tmpTypes[0].Substring(10, 1);
            us.MessageSubclass = tmpTypes[0].Substring(11, 1);

            us.Luno = tmpTypes[1];
            int i = 3;

            us.Dig = tmpTypes[i].Substring(0, 1);
            us.DeviceStatus = tmpTypes[i].Substring(1, 1);
            us.ErrorCode = tmpTypes[i].Substring(2, 1);
            us.EscrowCounts = tmpTypes[i].Substring(3, 50);
            us.VaultedCounts = tmpTypes[i].Substring(53, 50);
            us.ReturnCounts = tmpTypes[i].Substring(103, 50);
            us.NotesReturnedExitSlot = tmpTypes[i].Substring(153, 1);
            us.NotesInEscrow = tmpTypes[i].Substring(154, 1);

            if (tmpTypes[i].Length > 155)
                us.JustVaulted = tmpTypes[i].Substring(155, 1);

            if (tmpTypes[i].Length > 159)
            {
                us.EscrowCountsNoteType = tmpTypes[i].Substring(156, 2);
                us.EscrowCountsDecValue = tmpTypes[i].Substring(158, 3);
            }

            if (tmpTypes[i].Length > 164)
            {
                us.VoltedCountsNoteType = tmpTypes[i].Substring(161, 2);
                us.VoltedCountsDecValue = tmpTypes[i].Substring(163, 3);
            }

            if (tmpTypes[i].Length > 169)
            {
                us.ReturnCountsNoteType = tmpTypes[i].Substring(166, 2);
                us.ReturnCountsDecValue = tmpTypes[i].Substring(168, 3);
            }

            if (tmpTypes[i].Length > 172)
                us.NotesReturnedExcessNinety = tmpTypes[i].Substring(171, 3);

            if (tmpTypes[i].Length > 175)
                us.NotesInEscrowExcessNinety = tmpTypes[i].Substring(174, 3);

            if (tmpTypes[i].Length > 178)
                us.JustVaultedExcessNinety = tmpTypes[i].Substring(177, 3);


            if (tmpTypes.Length > i + 1)
                us.ErrorSeverity = tmpTypes[i + 1];

            if (tmpTypes.Length > i + 2)
                us.DiagnosticStatus = tmpTypes[i + 2];

            if (tmpTypes.Length > i + 3)
                us.SuppliesStatus = tmpTypes[i + 3];

            return us;
        }
    }
}

