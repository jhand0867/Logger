using Logger.GUI;
using System;
using System.Collections.Generic;

namespace Logger
{
    public static class LoggerFactory
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Project Create_Project()
        {
            return new Project();
        }

        public static ProjectInfo Create_ProjectInfo()
        {
            return new ProjectInfo();
        }

        public static List<typeRec> Create_ListOfTypeRec()
        {
            return new List<typeRec>();
        }

        public static StateRec Create_StateRecord()
        {
            return new StateRec();
        }

        public static Digester Create_Digester()
        {
            return new Digester();
        }

        public static SQLSearchCondition Create_SQLSearchCondition()
        {
            return new SQLSearchCondition();
        }

        public static SQLSearchCondition Create_SQLSearchCondition(string _field, string _cond, string _value, string _AndOr, string _fieldOutput)
        {
            return new SQLSearchCondition("", "", "", "", "");
        }

        public static LoadQuery Create_LoadQuery()
        {
            return new LoadQuery();
        }

        public static DeleteQuery Create_DeleteQuery()
        {
            return new DeleteQuery();
        }

        public static SaveQuery Create_SaveQuery(object _gridrows, string _queryName)
        {
            return new SaveQuery(_gridrows, _queryName);
        }


        public static StateRec Create_StateRecord(string stateType)
        {
            var stateTypeDic = new Dictionary<string, Func<StateRec>>();
            stateTypeDic.Add("A", () => new StateA());
            stateTypeDic.Add("B", () => new StateB());
            stateTypeDic.Add("D", () => new StateD());
            stateTypeDic.Add("E", () => new StateE());
            stateTypeDic.Add("F", () => new StateF());
            stateTypeDic.Add("G", () => new StateG());
            stateTypeDic.Add("H", () => new StateH());
            stateTypeDic.Add("I", () => new StateI());
            stateTypeDic.Add("J", () => new StateJ());
            stateTypeDic.Add("K", () => new StateK());
            stateTypeDic.Add("W", () => new StateW());
            stateTypeDic.Add("X", () => new StateX());
            stateTypeDic.Add("Y", () => new StateY());
            stateTypeDic.Add("Z", () => new StateZ());
            stateTypeDic.Add("&", () => new State26());
            stateTypeDic.Add("+", () => new State2B());
            stateTypeDic.Add(",", () => new State2C());
            stateTypeDic.Add("-", () => new State2D());
            stateTypeDic.Add(".", () => new State2E());
            stateTypeDic.Add("/", () => new State2F());
            stateTypeDic.Add(";", () => new State3B());
            stateTypeDic.Add(">", () => new State3E());
            stateTypeDic.Add("?", () => new State3F());
            stateTypeDic.Add("_", () => new State5F());
            stateTypeDic.Add("k", () => new State6B());
            stateTypeDic.Add("w", () => new State77());

            try
            {
                return stateTypeDic[stateType]();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IMessage Create_Record(string recType)
        {
            var recTypeDic = new Dictionary<string, Func<IMessage>>();
            recTypeDic.Add("11", () => new TRec());
            recTypeDic.Add("4", () => new TReply());
            recTypeDic.Add("311", () => new screenRec());
            recTypeDic.Add("312", () => new StateRec());
            recTypeDic.Add("313", () => new configParamsRec());
            recTypeDic.Add("315", () => new FitRec());
            recTypeDic.Add("316", () => new ConfigIdRec());
            recTypeDic.Add("31A", () => new EnhancedParamsRec());
            recTypeDic.Add("31B", () => new MACFieldSelection());
            recTypeDic.Add("31C", () => new DateAndTimeRec());
            recTypeDic.Add("31E", () => new DispenserMapping());
            recTypeDic.Add("32", () => new InteractiveTranResponse());
            recTypeDic.Add("34", () => new ExtEncryption());
            recTypeDic.Add("81", () => new ICCCurrencyDOT());
            recTypeDic.Add("82", () => new ICCTransactionDOT());
            recTypeDic.Add("83", () => new ICCLanguageSupportT());
            recTypeDic.Add("84", () => new ICCTerminalDOT());
            recTypeDic.Add("85", () => new ICCApplicationIDT());
            recTypeDic.Add("22", () => new SolicitedStatus());
            recTypeDic.Add("22A", () => new SolicitedStatus9());
            recTypeDic.Add("22B", () => new SolicitedStatusB());
            recTypeDic.Add("22C", () => new SolicitedStatusC());
            recTypeDic.Add("228", () => new SolicitedStatus8());
            recTypeDic.Add("229", () => new SolicitedStatus9());
            recTypeDic.Add("22F1", () => new SolicitedStatusF1());
            recTypeDic.Add("22F2", () => new SolicitedStatusF2());
            recTypeDic.Add("22F3", () => new SolicitedStatusF3());
            recTypeDic.Add("22F4", () => new SolicitedStatusF4());
            recTypeDic.Add("22F5", () => new SolicitedStatusF5());
            recTypeDic.Add("22F6", () => new SolicitedStatusF6());
            recTypeDic.Add("22F7", () => new SolicitedStatusF7());
            recTypeDic.Add("22FH", () => new SolicitedStatusFH());
            recTypeDic.Add("22FI", () => new SolicitedStatusFI());
            recTypeDic.Add("22FJ", () => new SolicitedStatusFJ());
            recTypeDic.Add("22FK", () => new SolicitedStatusFK());
            recTypeDic.Add("22FL", () => new SolicitedStatusFL());
            recTypeDic.Add("22FM", () => new SolicitedStatusFM());
            recTypeDic.Add("22FN", () => new SolicitedStatusFN());
            recTypeDic.Add("12", () => new UnsolicitedStatus());
            recTypeDic.Add("12A", () => new UnsolicitedStatusA());
            recTypeDic.Add("12B", () => new UnsolicitedStatusB());
            recTypeDic.Add("12D", () => new UnsolicitedStatusD());
            recTypeDic.Add("12E", () => new UnsolicitedStatusE());
            recTypeDic.Add("12F", () => new UnsolicitedStatusF());
            recTypeDic.Add("12G", () => new UnsolicitedStatusG());
            recTypeDic.Add("12H", () => new UnsolicitedStatusH());
            recTypeDic.Add("12K", () => new UnsolicitedStatusK());
            //recTypeDic.Add("12L", () => new UnsolicitedStatusL());
            //recTypeDic.Add("12M", () => new UnsolicitedStatusM());
            //recTypeDic.Add("12P", () => new UnsolicitedStatusP());
            //recTypeDic.Add("12Q", () => new UnsolicitedStatusQ());
            //recTypeDic.Add("12R", () => new UnsolicitedStatusR());
            //recTypeDic.Add("12S", () => new UnsolicitedStatusS());
            //recTypeDic.Add("12V", () => new UnsolicitedStatusV());
            //recTypeDic.Add("12Y", () => new UnsolicitedStatusY());
            //recTypeDic.Add("1261", () => new UnsolicitedStatus61());
            //recTypeDic.Add("125C", () => new UnsolicitedStatus5C());
            //recTypeDic.Add("1266", () => new UnsolicitedStatus66());
            //recTypeDic.Add("1271", () => new UnsolicitedStatus71());
            //recTypeDic.Add("12C", () => new UnsolicitedStatusC());
            recTypeDic.Add("23", () => new EncryptorInitData());
            recTypeDic.Add("231", () => new EncryptorInitData1());
            recTypeDic.Add("232", () => new EncryptorInitData2());
            recTypeDic.Add("233", () => new EncryptorInitData3());
            recTypeDic.Add("234", () => new EncryptorInitData4());
            //recTypeDic.Add("235", () => new EncryptorInitData5());
            recTypeDic.Add("236", () => new EncryptorInitData6());
            recTypeDic.Add("237", () => new EncryptorInitData7());
            recTypeDic.Add("238", () => new EncryptorInitData8());
            recTypeDic.Add("239", () => new EncryptorInitData9());
            recTypeDic.Add("23A", () => new EncryptorInitDataA());
            recTypeDic.Add("23B", () => new EncryptorInitDataB());
            // recTypeDic.Add("23C", () => new EncryptorInitDataC());
            recTypeDic.Add("23D", () => new EncryptorInitDataD());
            recTypeDic.Add("23E", () => new EncryptorInitDataE());
            recTypeDic.Add("61H", () => new UploadEjData());
            recTypeDic.Add("61J", () => new AckEjUploadBlock());
            recTypeDic.Add("62", () => new AckStopEj());
            recTypeDic.Add("63", () => new EjOptionsTimers());
            recTypeDic.Add("1", () => new TerminalCommands());
            try
            {
                return recTypeDic[recType]();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

    }
}
