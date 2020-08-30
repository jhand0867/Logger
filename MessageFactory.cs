using System;
using System.Collections.Generic;

namespace Logger
{
    public static class MessageFactory
    {

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
            recTypeDic.Add("00", () => new TRec());
            recTypeDic.Add("01", () => new TReply());
            recTypeDic.Add("11", () => new screenRec());
            recTypeDic.Add("12", () => new StateRec());
            recTypeDic.Add("13", () => new configParamsRec());
            recTypeDic.Add("15", () => new FitRec());
            recTypeDic.Add("16", () => new ConfigIdRec());
            recTypeDic.Add("1A", () => new EnhancedParamsRec());
            recTypeDic.Add("1C", () => new DateAndTimeRec());
            recTypeDic.Add("42", () => new ExtEncryptionRec());
            recTypeDic.Add("81", () => new ICCCurrencyDOT());
            recTypeDic.Add("82", () => new ICCTransactionDOT());
            recTypeDic.Add("83", () => new ICCLanguageSupportT());
            recTypeDic.Add("84", () => new ICCTerminalDOT());
            recTypeDic.Add("85", () => new ICCApplicationIDT());

            try
            {
                return recTypeDic[recType]();
            }
            catch (Exception ex)
            {
                return null;
            }

            //TODO:    case "1B":
            //        //writeMAC(typeList);
            //        return null;              
            //    case "1E":
            //        //writeDispenser(typeList);
            //        return null;

        }

    }
}
