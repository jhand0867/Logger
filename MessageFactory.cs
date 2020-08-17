using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public static class MessageFactory
    {

        public static Project Create_Project()
        {
            return new Project();
        }

        public static List<typeRec> Create_ListOfTypeRec()
        {
            return new List<typeRec>();
        }

        public static IMessage Create_Record(string recType)
        {
            var recTypeDic = new Dictionary<string, Func<IMessage>>();
            recTypeDic.Add("00", () => new TRec());
            recTypeDic.Add("01", () => new TReply());
            recTypeDic.Add("11", () => new screenRec());
            recTypeDic.Add("12", () => new stateRec());
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
            } catch (Exception ex)
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
