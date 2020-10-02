using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct solicitedStaF7
    {
        private string rectype;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string messageIdentifier;
        private string transactionGroupIdA;
        private string transactionSerialNumber;
        private string accumulatedTransactionCount;
        private string cardReaderDGIdB;
        private string cardsCaptured;
	    private string cashHandler0DGIdC;
	    private string cassetteTypeC;
	    private string notesInCassetteC;
	    private string notesRejectedC;
	    private string notesDispensedC;
	    private string lastTransNotesDispensedC;
    	private string notesDepositedC;
	    private string cashHandler1DGIdD;
	    private string cassetteTypeD;
	    private string notesInCassetteD;
	    private string notesRejectedD;
	    private string notesDispensedD;
	    private string lastTransNotesDispensedD;
	    private string notesDepositedD;
	    private string coinDispenserDGIdE;
	    private string hopperTypeNumber;
	    private string coinsRemaining;
	    private string coinsDispensed;
	    private string lastTransCoinsDispensed;
	    private string coinsDeposited;
	    private string envelopeDepositoryDGIdF;
	    private string envelopesDeposited;
	    private string lastEnvelopeSerialNumber;
	    private string cameraDGIdG;
	    private string cameraFilmRemaining;
	    private string bnaCassetteCountDGIdI;
	    private string ndcCassetteType;
	    private string totalNotesInCassette;
	    private string numberOfNotesTypeReported;
	    private string noteTypeIdentifier;
	    private string numberOfNotes;
    	private string chequeProcessorDGIdJ;
	    private string binNumber;
	    private string chequeDepositedInBin;
	    private string bnaEmulationDepositedDGIdK;
	    private string totalNotesRefunded;
	    private string totalNotesReturnedRejected;
	    private string totalNotesEncashed;
	    private string totalNotesEscrowed;
	    private string dualDispenserCombinedDGIdI;
	    private string cassetteTypeI;
	    private string notesInCassetteI;
	    private string notesRejectedI;
	    private string notesDispensedI;
	    private string lastTransNotesDispensedI;
	    private string notesDepositedI;
	    private string ecb6Cat2NotesDGIdN;
	    private string ndcCassetteTypeN;
	    private string totalNumberCat2Notes;
	    private string numberOfReportedCat2NoteTypes;
	    private string cat2NoteTypeIdentifier;
	    private string cat2Notes;
	    private string ecb6Cat3NotesDGIdO;
	    private string ndcCassetteTypeO;
	    private string totalNumberCat3Notes;
	    private string numberOfReportedCat3NoteTypes;
	    private string cat3NoteTypeIdentifier;
    	private string cat3Notes;
	    private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
        public string TransactionGroupIdA { get => transactionGroupIdA; set => transactionGroupIdA = value; }
        public string TransactionSerialNumber { get => transactionSerialNumber; set => transactionSerialNumber = value; }
        public string AccumulatedTransactionCount { get => accumulatedTransactionCount; set => accumulatedTransactionCount = value; }
        public string CardReaderDGIdB { get => cardReaderDGIdB; set => cardReaderDGIdB = value; }
        public string CardsCaptured { get => cardsCaptured; set => cardsCaptured = value; }
        public string CashHandler0DGIdC { get => cashHandler0DGIdC; set => cashHandler0DGIdC = value; }
        public string CassetteTypeC { get => cassetteTypeC; set => cassetteTypeC = value; }
        public string NotesInCassetteC { get => notesInCassetteC; set => notesInCassetteC = value; }
        public string NotesRejectedC { get => notesRejectedC; set => notesRejectedC = value; }
        public string NotesDispensedC { get => notesDispensedC; set => notesDispensedC = value; }
        public string LastTransNotesDispensedC { get => lastTransNotesDispensedC; set => lastTransNotesDispensedC = value; }
        public string NotesDepositedC { get => notesDepositedC; set => notesDepositedC = value; }
        public string CashHandler1DGIdD { get => cashHandler1DGIdD; set => cashHandler1DGIdD = value; }
        public string CassetteTypeD { get => cassetteTypeD; set => cassetteTypeD = value; }
        public string NotesInCassetteD { get => notesInCassetteD; set => notesInCassetteD = value; }
        public string NotesRejectedD { get => notesRejectedD; set => notesRejectedD = value; }
        public string NotesDispensedD { get => notesDispensedD; set => notesDispensedD = value; }
        public string LastTransNotesDispensedD { get => lastTransNotesDispensedD; set => lastTransNotesDispensedD = value; }
        public string NotesDepositedD { get => notesDepositedD; set => notesDepositedD = value; }
        public string CoinDispenserDGIdE { get => coinDispenserDGIdE; set => coinDispenserDGIdE = value; }
        public string HopperTypeNumber { get => hopperTypeNumber; set => hopperTypeNumber = value; }
        public string CoinsRemaining { get => coinsRemaining; set => coinsRemaining = value; }
        public string CoinsDispensed { get => coinsDispensed; set => coinsDispensed = value; }
        public string LastTransCoinsDispensed { get => lastTransCoinsDispensed; set => lastTransCoinsDispensed = value; }
        public string CoinsDeposited { get => coinsDeposited; set => coinsDeposited = value; }
        public string EnvelopeDepositoryDGIdF { get => envelopeDepositoryDGIdF; set => envelopeDepositoryDGIdF = value; }
        public string EnvelopesDeposited { get => envelopesDeposited; set => envelopesDeposited = value; }
        public string LastEnvelopeSerialNumber { get => lastEnvelopeSerialNumber; set => lastEnvelopeSerialNumber = value; }
        public string CameraDGIdG { get => cameraDGIdG; set => cameraDGIdG = value; }
        public string CameraFilmRemaining { get => cameraFilmRemaining; set => cameraFilmRemaining = value; }
        public string BnaCassetteCountDGIdI { get => bnaCassetteCountDGIdI; set => bnaCassetteCountDGIdI = value; }
        public string NdcCassetteType { get => ndcCassetteType; set => ndcCassetteType = value; }
        public string TotalNotesInCassette { get => totalNotesInCassette; set => totalNotesInCassette = value; }
        public string NumberOfNotesTypeReported { get => numberOfNotesTypeReported; set => numberOfNotesTypeReported = value; }
        public string NoteTypeIdentifier { get => noteTypeIdentifier; set => noteTypeIdentifier = value; }
        public string NumberOfNotes { get => numberOfNotes; set => numberOfNotes = value; }
        public string ChequeProcessorDGIdJ { get => chequeProcessorDGIdJ; set => chequeProcessorDGIdJ = value; }
        public string BinNumber { get => binNumber; set => binNumber = value; }
        public string ChequeDepositedInBin { get => chequeDepositedInBin; set => chequeDepositedInBin = value; }
        public string BnaEmulationDepositedDGIdK { get => bnaEmulationDepositedDGIdK; set => bnaEmulationDepositedDGIdK = value; }
        public string TotalNotesRefunded { get => totalNotesRefunded; set => totalNotesRefunded = value; }
        public string TotalNotesReturnedRejected { get => totalNotesReturnedRejected; set => totalNotesReturnedRejected = value; }
        public string TotalNotesEncashed { get => totalNotesEncashed; set => totalNotesEncashed = value; }
        public string TotalNotesEscrowed { get => totalNotesEscrowed; set => totalNotesEscrowed = value; }
        public string DualDispenserCombinedDGIdI { get => dualDispenserCombinedDGIdI; set => dualDispenserCombinedDGIdI = value; }
        public string CassetteTypeI { get => cassetteTypeI; set => cassetteTypeI = value; }
        public string NotesInCassetteI { get => notesInCassetteI; set => notesInCassetteI = value; }
        public string NotesRejectedI { get => notesRejectedI; set => notesRejectedI = value; }
        public string NotesDispensedI { get => notesDispensedI; set => notesDispensedI = value; }
        public string LastTransNotesDispensedI { get => lastTransNotesDispensedI; set => lastTransNotesDispensedI = value; }
        public string NotesDepositedI { get => notesDepositedI; set => notesDepositedI = value; }
        public string Ecb6Cat2NotesDGIdN { get => ecb6Cat2NotesDGIdN; set => ecb6Cat2NotesDGIdN = value; }
        public string NdcCassetteTypeN { get => ndcCassetteTypeN; set => ndcCassetteTypeN = value; }
        public string TotalNumberCat2Notes { get => totalNumberCat2Notes; set => totalNumberCat2Notes = value; }
        public string NumberOfReportedCat2NoteTypes { get => numberOfReportedCat2NoteTypes; set => numberOfReportedCat2NoteTypes = value; }
        public string Cat2NoteTypeIdentifier { get => cat2NoteTypeIdentifier; set => cat2NoteTypeIdentifier = value; }
        public string Cat2Notes { get => cat2Notes; set => cat2Notes = value; }
        public string Ecb6Cat3NotesDGIdO { get => ecb6Cat3NotesDGIdO; set => ecb6Cat3NotesDGIdO = value; }
        public string NdcCassetteTypeO { get => ndcCassetteTypeO; set => ndcCassetteTypeO = value; }
        public string TotalNumberCat3Notes { get => totalNumberCat3Notes; set => totalNumberCat3Notes = value; }
        public string NumberOfReportedCat3NoteTypes { get => numberOfReportedCat3NoteTypes; set => numberOfReportedCat3NoteTypes = value; }
        public string Cat3NoteTypeIdentifier { get => cat3NoteTypeIdentifier; set => cat3NoteTypeIdentifier = value; }
        public string Cat3Notes { get => cat3Notes; set => cat3Notes = value; }
        public string Mac { get => mac; set => mac = value; }
    };

    class SolicitedStatusF7 : EMVConfiguration, IMessage
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
                solicitedStaF7 ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusF7[logkey],[rectype],
	                        [luno],[timeVariant],[statusDescriptor],[messageIdentifier],[transactionGroupIdA],
                        	[transactionSerialNumber],[accumulatedTransactionCount],[cardReaderDGIdB],[cardsCaptured],
	                        [cashHandler0DGIdC],[cassetteTypeC],[notesInCassetteC],[notesRejectedC],[notesDispensedC],
                        	[lastTransNotesDispensedC],[notesDepositedC],[cashHandler1DGIdD],[cassetteTypeD],[notesInCassetteD],
                        	[notesRejectedD],[notesDispensedD],[lastTransNotesDispensedD],[notesDepositedD],[coinDispenserDGIdE],
                        	[hopperTypeNumber],[coinsRemaining],[coinsDispensed],[lastTransCoinsDispensed],
                        	[coinsDeposited],[envelopeDepositoryDGIdF],[envelopesDeposited],[lastEnvelopeSerialNumber],
                        	[cameraDGIdG],[cameraFilmRemaining],[bnaCassetteCountDGIdI],[ndcCassetteType],[totalNotesInCassette],
                            [numberOfNotesTypeReported],[noteTypeIdentifier],[numberOfNotes],[chequeProcessorDGIdJ],[binNumber],
                        	[chequeDepositedInBin],[bnaEmulationDepositedDGIdK],[totalNotesRefunded],[totalNotesReturnedRejected],
                        	[totalNotesEncashed],[totalNotesEscrowed],[dualDispenserCombinedDGIdI],[cassetteTypeI],
                        	[notesInCassetteI],[notesRejectedI],[notesDispensedI],[lastTransNotesDispensedI],[notesDepositedI],
	                        [ecb6Cat2NotesDGIdN],[ndcCassetteTypeN],[totalNumberCat2Notes],[numberOfReportedCat2NoteTypes],
                        	[cat2NoteTypeIdentifier],[cat2Notes],[ecb6Cat3NotesDGIdO],[ndcCassetteTypeO],[totalNumberCat3Notes],
                            [numberOfReportedCat3NoteTypes],[cat3NoteTypeIdentifier],[cat3Notes],[mac],[prjkey],[logID]) " +
                    " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" +
                            ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                            ss.MessageIdentifier + "','" + ss.TransactionGroupIdA + "','" + ss.TransactionSerialNumber + "','" +
                            ss.AccumulatedTransactionCount + "','" + ss.CardReaderDGIdB + "','" + ss.CardsCaptured + "','" +
                            ss.CashHandler0DGIdC + "','" + ss.CassetteTypeC + "','" + ss.NotesInCassetteC + "','" + 
                            ss.NotesRejectedC + "','" + ss.NotesDispensedC + "','" + ss.LastTransNotesDispensedC + "','" + 
                            ss.NotesDepositedC + "','" + ss.CashHandler1DGIdD + "','" + ss.CassetteTypeD + "','" + 
                            ss.NotesInCassetteD + "','" + ss.NotesRejectedD + "','" + ss.NotesDispensedD + "','" + 
                            ss.LastTransNotesDispensedD + "','" + ss.NotesDepositedD + "','" + ss.CoinDispenserDGIdE + "','" +
                            ss.HopperTypeNumber + "','" + ss.CoinsRemaining + "','" + ss.CoinsDispensed + "','" + 
                            ss.LastTransCoinsDispensed + "','" + ss.CoinsDeposited + "','" + ss.EnvelopeDepositoryDGIdF + "','" +
                            ss.EnvelopesDeposited + "','" + ss.LastEnvelopeSerialNumber + "','" + ss.CameraDGIdG + "','" +
                            ss.CameraFilmRemaining + "','" + ss.BnaCassetteCountDGIdI + "','" + ss.NdcCassetteType + "','" +
                            ss.TotalNotesInCassette + "','" + ss.NumberOfNotesTypeReported + "','" + ss.NoteTypeIdentifier + "','" +
                            ss.NumberOfNotes + "','" + ss.ChequeProcessorDGIdJ + "','" + ss.BinNumber + "','" +
                            ss.ChequeDepositedInBin + "','" + ss.BnaEmulationDepositedDGIdK + "','" + ss.TotalNotesRefunded + "','" +
                            ss.TotalNotesReturnedRejected + "','" + ss.TotalNotesEncashed + "','" + ss.TotalNotesEscrowed + "','" +
                            ss.DualDispenserCombinedDGIdI + "','" + ss.CassetteTypeI + "','" + ss.NotesInCassetteI + "','" +
                            ss.NotesRejectedI + "','" + ss.NotesDispensedI + "','" + ss.LastTransNotesDispensedI + "','" +
                            ss.NotesDepositedI + "','" + ss.Ecb6Cat2NotesDGIdN + "','" + ss.NdcCassetteTypeN + "','" +
                            ss.TotalNumberCat2Notes + "','" + ss.NumberOfReportedCat2NoteTypes + "','" + 
                            ss.Cat2NoteTypeIdentifier + "','" + ss.Cat2Notes + "','" + ss.Ecb6Cat3NotesDGIdO + "','" +
                            ss.NdcCassetteTypeO + "','" + ss.TotalNumberCat3Notes + "','" + ss.NumberOfReportedCat3NoteTypes + "','" +
                            ss.Cat3NoteTypeIdentifier + "','" + ss.Cat3Notes + "','" +
                            ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedStaF7 parseData(string r)
        {
            solicitedStaF7 ss = new solicitedStaF7();
            string[] tmpTypes = r.Split((char)0x1c);

            ss.Rectype = "N";
            ss.Luno = tmpTypes[1];
            int i = 3;
            if (tmpTypes[3].Length != 1)
            {
                i = 4;
                ss.TimeVariant = tmpTypes[3];
            }

            // mlh HERE

            ss.StatusDescriptor = tmpTypes[i].Substring(0, 1);
            ss.MessageIdentificer = tmpTypes[i].Substring(1, 1);
            ss.GroupNumber = tmpTypes[i].Substring(2, 1);
            ss.NewEntries = tmpTypes[i].Substring(3, 2);
            ss.DateLastCleared = tmpTypes[i].Substring(5, 12);

            if (tmpTypes.Length > i + 1)
                ss.Mac = tmpTypes[i + 1];

            return ss;
        }
    }
}
