using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct solicitedStaF2
    {
        private string rectype;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string messageIdentifier;
        private string transactionSerialNumber;
        private string accumulatedTranCount;
        private string notesInCassette;
        private string notesRejected;
        private string notesDispensed;
        private string lastTranNotesDispensed;
        private string cardsCaptured;
        private string envelopesDeposited;
        private string cameraFilmRemaining;
        private string lastEnvelopeSerialNum;        
        private string reserved1;        
        private string reserved2;
        private string reserved3;
        private string reserved4;
        private string reserved5;
        private string coinsRemaining;
        private string coinsDispensed;
        private string lastTranCoinsDispensed;
        private string totalNotesRefunded;
        private string totalNotesRejected;
        private string totalNotesEncaged;
        private string totalNotesEscrow;
        private string reserved6;
        private string reserved7;
        private string reserved8;
        private string reserved9;
        private string chequesDepositedBin1;
        private string chequesDepositedBin2;
        private string chequesDepositedBin3;
        private string chequesDepositedBin4;
        private string reserved10;
        private string reserved11;
        private string reserved12;
        private string numberOfPassBooksCaptured;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
        public string TransactionSerialNumber { get => transactionSerialNumber; set => transactionSerialNumber = value; }
        public string AccumulatedTranCount { get => accumulatedTranCount; set => accumulatedTranCount = value; }
        public string NotesInCassette { get => notesInCassette; set => notesInCassette = value; }
        public string NotesRejected { get => notesRejected; set => notesRejected = value; }
        public string NotesDispensed { get => notesDispensed; set => notesDispensed = value; }
        public string LastTranNotesDispensed { get => lastTranNotesDispensed; set => lastTranNotesDispensed = value; }
        public string CardsCaptured { get => cardsCaptured; set => cardsCaptured = value; }
        public string EnvelopesDeposited { get => envelopesDeposited; set => envelopesDeposited = value; }
        public string CameraFilmRemaining { get => cameraFilmRemaining; set => cameraFilmRemaining = value; }
        public string LastEnvelopeSerialNum { get => lastEnvelopeSerialNum; set => lastEnvelopeSerialNum = value; }
        public string Reserved1 { get => reserved1; set => reserved1 = value; }
        public string Reserved2 { get => reserved2; set => reserved2 = value; }
        public string Reserved3 { get => reserved3; set => reserved3 = value; }
        public string Reserved4 { get => reserved4; set => reserved4 = value; }
        public string Reserved5 { get => reserved5; set => reserved5 = value; }
        public string CoinsRemaining { get => coinsRemaining; set => coinsRemaining = value; }
        public string CoinsDispensed { get => coinsDispensed; set => coinsDispensed = value; }
        public string LastTranCoinsDispensed { get => lastTranCoinsDispensed; set => lastTranCoinsDispensed = value; }
        public string TotalNotesRefunded { get => totalNotesRefunded; set => totalNotesRefunded = value; }
        public string TotalNotesRejected { get => totalNotesRejected; set => totalNotesRejected = value; }
        public string TotalNotesEncaged { get => totalNotesEncaged; set => totalNotesEncaged = value; }
        public string TotalNotesEscrow { get => totalNotesEscrow; set => totalNotesEscrow = value; }
        public string Reserved6 { get => reserved6; set => reserved6 = value; }
        public string Reserved7 { get => reserved7; set => reserved7 = value; }
        public string Reserved8 { get => reserved8; set => reserved8 = value; }
        public string Reserved9 { get => reserved9; set => reserved9 = value; }
        public string ChequesDepositedBin1 { get => chequesDepositedBin1; set => chequesDepositedBin1 = value; }
        public string ChequesDepositedBin2 { get => chequesDepositedBin2; set => chequesDepositedBin2 = value; }
        public string ChequesDepositedBin3 { get => chequesDepositedBin3; set => chequesDepositedBin3 = value; }
        public string ChequesDepositedBin4 { get => chequesDepositedBin4; set => chequesDepositedBin4 = value; }
        public string Reserved10 { get => reserved10; set => reserved10 = value; }
        public string Reserved11 { get => reserved11; set => reserved11 = value; }
        public string Reserved12 { get => reserved12; set => reserved12 = value; }
        public string NumberOfPassBooksCaptured { get => numberOfPassBooksCaptured; set => numberOfPassBooksCaptured = value; }
        public string Mac { get => mac; set => mac = value; }
    };

    class SolicitedStatusF2 : SolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                solicitedStaF2 ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusF2([logkey],[rectype],
	                        [luno],[timeVariant],[statusDescriptor],[messageIdentifier],[transactionSerialNumber],
	                        [accumulatedTranCount],[notesInCassette],[notesRejected],[notesDispensed],
	                        [lastTranNotesDispensed],[cardsCaptured],[envelopesDeposited],[cameraFilmRemaining],
	                        [lastEnvelopeSerialNum],[reserved1],[reserved2],[reserved3],[reserved4],[reserved5],
	                        [coinsRemaining],[coinsDispensed],[lastTranCoinsDispensed],[totalNotesRefunded],
                    	    [totalNotesRejected],[totalNotesEncaged],[totalNotesEscrow],[reserved6],[reserved7],
	                        [reserved8],[reserved9],[chequesDepositedBin1],[chequesDepositedBin2],[chequesDepositedBin3],
	                        [chequesDepositedBin4],[reserved10],[reserved11],[reserved12],[numberOfPassBooksCaptured],
                            [mac],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" +
                            ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                            ss.MessageIdentifier + "','" + ss.TransactionSerialNumber + "','" + ss.AccumulatedTranCount + "','" +
                            ss.NotesInCassette + "','" + ss.NotesRejected + "','" + ss.NotesDispensed + "','" +
                            ss.LastTranNotesDispensed + "','" + ss.CardsCaptured + "','" + ss.EnvelopesDeposited + "','" +
                            ss.CameraFilmRemaining + "','" + ss.LastEnvelopeSerialNum + "','" + ss.Reserved1 + "','" +
                            ss.Reserved2 + "','" + ss.Reserved3 + "','" + ss.Reserved4 + "','" +
                            ss.Reserved5 + "','" + ss.CoinsRemaining + "','" + ss.CoinsDispensed + "','" +
                            ss.LastTranCoinsDispensed + "','" + ss.TotalNotesRefunded + "','" + ss.TotalNotesRejected + "','" +
                            ss.TotalNotesEncaged + "','" + ss.TotalNotesEscrow + "','" + ss.Reserved6 + "','" +
                            ss.Reserved7 + "','" + ss.Reserved8 + "','" + ss.Reserved9 + "','" +
                            ss.ChequesDepositedBin1 + "','" + ss.ChequesDepositedBin2 + "','" + ss.ChequesDepositedBin3 + "','" +
                            ss.ChequesDepositedBin4 + "','" + ss.Reserved10 + "','" + ss.Reserved11 + "','" +
                            ss.Reserved12 + "','" + ss.NumberOfPassBooksCaptured + "','" + ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedStaF2 parseData(string r)
        {
            solicitedStaF2 ss = new solicitedStaF2();

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

            string[] statusInfo = tmpTypes[i+1].Split((char)0x1d);

            ss.MessageIdentifier = statusInfo[0].Substring(0, 1);
            ss.TransactionSerialNumber = statusInfo[0].Substring(1, 4); 
            ss.AccumulatedTranCount = statusInfo[0].Substring(5, 7);
            ss.NotesInCassette = statusInfo[0].Substring(12, 20); 
            ss.NotesRejected = statusInfo[0].Substring(32, 20);
            ss.NotesDispensed = statusInfo[0].Substring(52, 20);
            ss.LastTranNotesDispensed = statusInfo[0].Substring(72, 20);
            ss.CardsCaptured = statusInfo[0].Substring(92, 5);
            ss.EnvelopesDeposited = statusInfo[0].Substring(97, 5); 
            ss.CameraFilmRemaining = statusInfo[0].Substring(102, 5);
            ss.LastEnvelopeSerialNum = statusInfo[0].Substring(107, 5);

            if (statusInfo[0].Length > 112) ss.Reserved1 = statusInfo[0].Substring(112, 1);
            if (statusInfo.Length > 1) ss.Reserved2 = statusInfo[1];
            if (statusInfo.Length > 2) ss.Reserved3 = statusInfo[2];
            if (statusInfo.Length > 3) ss.Reserved4 = statusInfo[3];
            if (statusInfo.Length > 4) ss.Reserved5 = statusInfo[4];
            if (statusInfo.Length > 5)
            {
                ss.CoinsRemaining = statusInfo[5].Substring(0, 20);
                ss.CoinsDispensed = statusInfo[5].Substring(20, 20);
                ss.LastTranCoinsDispensed = statusInfo[5].Substring(40, 20);
            }
            if (statusInfo.Length > 6)
            {
                ss.TotalNotesRefunded = statusInfo[6].Substring(0,5);
                ss.TotalNotesRejected = statusInfo[6].Substring(5, 5);
                ss.TotalNotesEncaged = statusInfo[6].Substring(10, 5);
                ss.TotalNotesEscrow = statusInfo[6].Substring(15, 5);
            }
            if (statusInfo.Length > 7) ss.Reserved6 = statusInfo[7];
            if (statusInfo.Length > 8) ss.Reserved7 = statusInfo[8];
            if (statusInfo.Length > 9) ss.Reserved8 = statusInfo[9];
            if (statusInfo.Length > 10) ss.Reserved9 = statusInfo[10];
            if (statusInfo.Length > 11)
            {
                ss.ChequesDepositedBin1 = statusInfo[11].Substring(0,5);
                ss.ChequesDepositedBin2 = statusInfo[11].Substring(5, 5);
                ss.ChequesDepositedBin3 = statusInfo[11].Substring(10, 5);
                ss.ChequesDepositedBin4 = statusInfo[11].Substring(15, 5);
            }
            if (statusInfo.Length > 12) ss.Reserved10 = statusInfo[12];
            if (statusInfo.Length > 13) ss.Reserved11 = statusInfo[13];
            if (statusInfo.Length > 14) ss.Reserved12 = statusInfo[14];
            if (statusInfo.Length > 15) ss.NumberOfPassBooksCaptured = statusInfo[15];
            
            if (tmpTypes.Length > i+2)
                ss.Mac = tmpTypes[i+2];

            return ss;
        }
    }
}
