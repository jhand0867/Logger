using System.Collections.Generic;

namespace Logger
{
    struct solicitedStaF7
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
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
        private string cashHandler0DGDataC;
        private string cashHandler1DGIdD;
        private string cashHandler1DGDataD;
        private string coinDispenserDGIdE;
        private string coinDispenserDGDataE;
        private string envelopeDepositoryDGIdF;
        private string envelopesDeposited;
        private string lastEnvelopeSerialNumber;
        private string cameraDGIdG;
        private string cameraFilmRemaining;
        private string bnaCassetteCountDGIdI;
        private string ndcCassetteTypeData;
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
        private string dualDispenserCombinedDGIdL;
        private string dualDispenserCombinedData;
        private string ecb6Cat2NotesDGIdN;
        private string ecb6Cat2NotesData;
        private string cat2NoteTypeIdentifier;
        private string cat2Notes;
        private string ecb6Cat3NotesDGIdO;
        private string ecb6Cat3NotesData;
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
        public string CashHandler0DGDataC { get => cashHandler0DGDataC; set => cashHandler0DGDataC = value; }
        public string CashHandler1DGIdD { get => cashHandler1DGIdD; set => cashHandler1DGIdD = value; }
        public string CashHandler1DGDataD { get => cashHandler1DGDataD; set => cashHandler1DGDataD = value; }
        public string CoinDispenserDGIdE { get => coinDispenserDGIdE; set => coinDispenserDGIdE = value; }
        public string CoinDispenserDGDataE { get => coinDispenserDGDataE; set => coinDispenserDGDataE = value; }
        public string EnvelopeDepositoryDGIdF { get => envelopeDepositoryDGIdF; set => envelopeDepositoryDGIdF = value; }
        public string EnvelopesDeposited { get => envelopesDeposited; set => envelopesDeposited = value; }
        public string LastEnvelopeSerialNumber { get => lastEnvelopeSerialNumber; set => lastEnvelopeSerialNumber = value; }
        public string CameraDGIdG { get => cameraDGIdG; set => cameraDGIdG = value; }
        public string CameraFilmRemaining { get => cameraFilmRemaining; set => cameraFilmRemaining = value; }
        public string BnaCassetteCountDGIdI { get => bnaCassetteCountDGIdI; set => bnaCassetteCountDGIdI = value; }
        public string NdcCassetteTypeData { get => ndcCassetteTypeData; set => ndcCassetteTypeData = value; }
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
        public string DualDispenserCombinedDGIdL { get => dualDispenserCombinedDGIdL; set => dualDispenserCombinedDGIdL = value; }
        public string DualDispenserCombinedData { get => dualDispenserCombinedData; set => dualDispenserCombinedData = value; }
        public string Ecb6Cat2NotesDGIdN { get => ecb6Cat2NotesDGIdN; set => ecb6Cat2NotesDGIdN = value; }
        public string Ecb6Cat2NotesData { get => ecb6Cat2NotesData; set => ecb6Cat2NotesData = value; }
        public string Cat2NoteTypeIdentifier { get => cat2NoteTypeIdentifier; set => cat2NoteTypeIdentifier = value; }
        public string Cat2Notes { get => cat2Notes; set => cat2Notes = value; }
        public string Ecb6Cat3NotesDGIdO { get => ecb6Cat3NotesDGIdO; set => ecb6Cat3NotesDGIdO = value; }
        public string Ecb6Cat3NotesData { get => ecb6Cat3NotesData; set => ecb6Cat3NotesData = value; }
        public string Cat3NoteTypeIdentifier { get => cat3NoteTypeIdentifier; set => cat3NoteTypeIdentifier = value; }
        public string Cat3Notes { get => cat3Notes; set => cat3Notes = value; }
        public string Mac { get => mac; set => mac = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    };

    class SolicitedStatusF7 : SolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                solicitedStaF7 ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusF7[logkey],[rectype],[messageClass],[messageSubClass],
	                        [luno],[timeVariant],[statusDescriptor],[messageIdentifier],[transactionGroupIdA],
                        	[transactionSerialNumber],[accumulatedTransactionCount],[cardReaderDGIdB],[cardsCaptured],
	                        [cashHandler0DGIdC],[cashHandler0DGDataC],[cashHandler1DGIdD],[cashHandler1DGDataD],
                            [coinDispenserDGIdE],[coinDispenserDGDataE],[envelopeDepositoryDGIdF],[envelopesDeposited],
                            [lastEnvelopeSerialNumber],[cameraDGIdG],[cameraFilmRemaining],[bnaCassetteCountDGIdI],
                            [ndcCassetteTypeData],[noteTypeIdentifier],[numberOfNotes],[chequeProcessorDGIdJ],[binNumber],
                        	[chequeDepositedInBin],[bnaEmulationDepositedDGIdK],[totalNotesRefunded],[totalNotesReturnedRejected],
                        	[totalNotesEncashed],[totalNotesEscrowed],[dualDispenserCombinedDGIdL],[dualDispenserCombinedData],                        	
	                        [ecb6Cat2NotesDGIdN],[ecb6Cat2NotesData],[cat2NoteTypeIdentifier],[cat2Notes],[ecb6Cat3NotesDGIdO],
                            [ecb6Cat3NotesData],[cat3NoteTypeIdentifier],[cat3Notes],[mac],[prjkey],[logID]) " +
                    " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" + ss.MessageClass + "','" + ss.MessageSubClass + "','" +
                            ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                            ss.MessageIdentifier + "','" + ss.TransactionGroupIdA + "','" + ss.TransactionSerialNumber + "','" +
                            ss.AccumulatedTransactionCount + "','" + ss.CardReaderDGIdB + "','" + ss.CardsCaptured + "','" +
                            ss.CashHandler0DGIdC + "','" + ss.CashHandler0DGDataC + "','" + ss.CashHandler1DGIdD + "','" +
                            ss.CashHandler1DGDataD + "','" + ss.CoinDispenserDGIdE + "','" + ss.CoinDispenserDGDataE + "','" +
                            ss.EnvelopeDepositoryDGIdF + "','" + ss.EnvelopesDeposited + "','" + ss.LastEnvelopeSerialNumber + "','" +
                            ss.CameraDGIdG + "','" + ss.CameraFilmRemaining + "','" + ss.BnaCassetteCountDGIdI + "','" +
                            ss.NdcCassetteTypeData + "','" + ss.NoteTypeIdentifier + "','" + ss.NumberOfNotes + "','" +
                            ss.ChequeProcessorDGIdJ + "','" + ss.BinNumber + "','" + ss.ChequeDepositedInBin + "','" +
                            ss.BnaEmulationDepositedDGIdK + "','" + ss.TotalNotesRefunded + "','" + ss.TotalNotesReturnedRejected + "','" +
                            ss.TotalNotesEncashed + "','" + ss.TotalNotesEscrowed + "','" +
                            ss.DualDispenserCombinedDGIdL + "','" + ss.DualDispenserCombinedData + "','" + ss.Ecb6Cat2NotesDGIdN + "','" +
                            ss.Ecb6Cat2NotesData + "','" + ss.Cat2NoteTypeIdentifier + "','" + ss.Cat2Notes + "','" + ss.Ecb6Cat3NotesDGIdO + "','" +
                            ss.Ecb6Cat3NotesData + "','" + ss.Cat3NoteTypeIdentifier + "','" + ss.Cat3Notes + "','" +
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
            ss.MessageClass = tmpTypes[0].Substring(10, 1);
            ss.MessageSubClass = tmpTypes[0].Substring(11, 1);
            ss.Luno = tmpTypes[1];
            int i = 3;
            if (tmpTypes[3].Length != 1)
            {
                i = 4;
                ss.TimeVariant = tmpTypes[3];
            }

            ss.StatusDescriptor = tmpTypes[i].Substring(0, 1);
            string[] statusInfo = tmpTypes[i + 1].Split((char)0x1d);

            ss.MessageIdentifier = statusInfo[0].Substring(0, 1);
            ss.TransactionGroupIdA = statusInfo[0].Substring(1, 1);
            ss.TransactionSerialNumber = statusInfo[0].Substring(2, 4);
            ss.AccumulatedTransactionCount = statusInfo[0].Substring(6, 7);
            ss.CardReaderDGIdB = statusInfo[1].Substring(0, 1);
            ss.CardsCaptured = statusInfo[1].Substring(1, 5);
            ss.CashHandler0DGIdC = statusInfo[2].Substring(0, 1);

            int numEntries = (statusInfo[2].Length - 1) / 28;
            int offset = 1;
            ss.CashHandler0DGDataC = "";
            for (int x = 0; x < numEntries; x++)
            {
                ss.CashHandler0DGDataC += statusInfo[2].Substring(offset, 3) + " ";
                offset += 3;
                ss.CashHandler0DGDataC += statusInfo[2].Substring(offset, 5) + " ";
                offset += 5;
                ss.CashHandler0DGDataC += statusInfo[2].Substring(offset, 5) + " ";
                offset += 5;
                ss.CashHandler0DGDataC += statusInfo[2].Substring(offset, 5) + " ";
                offset += 5;
                ss.CashHandler0DGDataC += statusInfo[2].Substring(offset, 5) + " ";
                offset += 5;
                ss.CashHandler0DGDataC += statusInfo[2].Substring(offset, 5) + ";";
                offset += 5;
            }

            ss.CashHandler1DGIdD = statusInfo[3].Substring(0, 1);
            numEntries = (statusInfo[3].Length - 1) / 28;
            offset = 1;
            ss.CashHandler1DGDataD = "";
            for (int x = 0; x < numEntries; x++)
            {
                ss.CashHandler1DGDataD += statusInfo[3].Substring(offset, 3) + " ";
                offset += 3;
                ss.CashHandler1DGDataD += statusInfo[3].Substring(offset, 5) + " ";
                offset += 5;
                ss.CashHandler1DGDataD += statusInfo[3].Substring(offset, 5) + " ";
                offset += 5;
                ss.CashHandler1DGDataD += statusInfo[3].Substring(offset, 5) + " ";
                offset += 5;
                ss.CashHandler1DGDataD += statusInfo[3].Substring(offset, 5) + " ";
                offset += 5;
                ss.CashHandler1DGDataD += statusInfo[3].Substring(offset, 5) + ";";
                offset += 5;
            }

            ss.CoinDispenserDGIdE = statusInfo[4].Substring(0, 1);
            numEntries = (statusInfo[4].Length - 1) / 22;
            offset = 1;
            ss.CoinDispenserDGDataE = "";
            for (int x = 0; x < numEntries; x++)
            {
                ss.CoinDispenserDGDataE += statusInfo[4].Substring(offset, 2) + " ";
                offset += 2;
                ss.CoinDispenserDGDataE += statusInfo[4].Substring(offset, 5) + " ";
                offset += 5;
                ss.CoinDispenserDGDataE += statusInfo[4].Substring(offset, 5) + " ";
                offset += 5;
                ss.CoinDispenserDGDataE += statusInfo[4].Substring(offset, 5) + " ";
                offset += 5;
                ss.CoinDispenserDGDataE += statusInfo[4].Substring(offset, 5) + ";";
                offset += 5;

            }

            ss.EnvelopeDepositoryDGIdF = statusInfo[5].Substring(0, 1);
            ss.EnvelopesDeposited = statusInfo[5].Substring(1, 5);
            ss.LastEnvelopeSerialNumber = statusInfo[5].Substring(6, 5);

            ss.CameraDGIdG = statusInfo[6].Substring(0, 1);
            ss.CameraFilmRemaining = statusInfo[6].Substring(1, 5);

            ss.BnaCassetteCountDGIdI = statusInfo[7].Substring(0, 1);
            numEntries = (statusInfo[7].Length - 1 - 9) / 11;
            offset = 1;
            ss.NdcCassetteTypeData = "";
            for (int x = 0; x < numEntries; x++)
            {
                ss.NdcCassetteTypeData += statusInfo[7].Substring(offset, 3) + " ";
                offset += 3;
                ss.NdcCassetteTypeData += statusInfo[7].Substring(offset, 5) + " ";
                offset += 5;
                ss.NdcCassetteTypeData += statusInfo[7].Substring(offset, 3) + ";";
                offset += 3;
            }

            ss.NoteTypeIdentifier = statusInfo[7].Substring(offset, 4);
            offset += 4;
            ss.NumberOfNotes = statusInfo[7].Substring(offset, 5);

            ss.ChequeProcessorDGIdJ = statusInfo[8].Substring(0, 1);
            ss.BinNumber = statusInfo[8].Substring(1, 1);
            ss.ChequeDepositedInBin = statusInfo[8].Substring(2, 5);

            ss.BnaEmulationDepositedDGIdK = statusInfo[9].Substring(0, 1);
            ss.TotalNotesRefunded = statusInfo[9].Substring(1, 5);
            ss.TotalNotesReturnedRejected = statusInfo[9].Substring(6, 5);
            ss.TotalNotesEncashed = statusInfo[9].Substring(11, 5);
            ss.TotalNotesEscrowed = statusInfo[9].Substring(16, 1);

            ss.DualDispenserCombinedDGIdL = statusInfo[10].Substring(0, 1);
            numEntries = (statusInfo[10].Length - 1) / 28;
            offset = 1;
            ss.DualDispenserCombinedData = "";
            for (int x = 0; x < numEntries; x++)
            {
                ss.DualDispenserCombinedData += statusInfo[10].Substring(offset, 3) + " ";
                offset += 3;
                ss.DualDispenserCombinedData += statusInfo[10].Substring(offset, 5) + " ";
                offset += 5;
                ss.DualDispenserCombinedData += statusInfo[10].Substring(offset, 5) + " ";
                offset += 5;
                ss.DualDispenserCombinedData += statusInfo[10].Substring(offset, 5) + " ";
                offset += 5;
                ss.DualDispenserCombinedData += statusInfo[10].Substring(offset, 5) + " ";
                offset += 5;
                ss.DualDispenserCombinedData += statusInfo[10].Substring(offset, 5) + ";";
                offset += 5;
            }

            ss.Ecb6Cat2NotesDGIdN = statusInfo[11].Substring(0, 5);
            numEntries = (statusInfo[11].Length - 1 - 9) / 11;
            offset = 1;
            ss.Ecb6Cat2NotesData = "";
            for (int x = 0; x < numEntries; x++)
            {
                ss.Ecb6Cat2NotesData += statusInfo[11].Substring(offset, 3) + " ";
                offset += 3;
                ss.Ecb6Cat2NotesData += statusInfo[11].Substring(offset, 5) + " ";
                offset += 5;
                ss.Ecb6Cat2NotesData += statusInfo[11].Substring(offset, 3) + ";";
                offset += 3;
            }
            ss.Cat2NoteTypeIdentifier = statusInfo[11].Substring(offset, 4);
            offset += 4;
            ss.Cat2Notes = statusInfo[11].Substring(offset, 5);

            ss.Ecb6Cat3NotesDGIdO = statusInfo[12].Substring(0, 5);
            numEntries = (statusInfo[12].Length - 1 - 9) / 11;
            offset = 1;
            ss.Ecb6Cat3NotesData = "";
            for (int x = 0; x < numEntries; x++)
            {
                ss.Ecb6Cat3NotesData += statusInfo[12].Substring(offset, 3) + " ";
                offset += 3;
                ss.Ecb6Cat3NotesData += statusInfo[12].Substring(offset, 5) + " ";
                offset += 5;
                ss.Ecb6Cat3NotesData += statusInfo[12].Substring(offset, 3) + ";";
                offset += 3;
            }
            ss.Cat3NoteTypeIdentifier = statusInfo[12].Substring(offset, 4);
            offset += 4;
            ss.Cat3Notes = statusInfo[12].Substring(offset, 5);

            if (tmpTypes.Length > i + 1)
                ss.Mac = tmpTypes[i + 1];

            return ss;
        }
    }
}
