using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class StateJ : stateRec
    {
        public override void ValidateState(stateRec stateData)
        {

            Dictionary<string, stateRec> resultData = new Dictionary<string, stateRec>();

            base.ValidateState(stateData);

            if (stateData.stateType == "J")
            {

            }
        }
        public string getInfo(stateRec stRec)
        {
            string fieldData = "State Number: " + stRec.StateNumber + System.Environment.NewLine;
            fieldData += "  State Type: " + stRec.StateType + System.Environment.NewLine;
            fieldData += "  With Receipt Screen Number:       " + stRec.Val1 + System.Environment.NewLine;
            fieldData += "  Next State Number:                " + stRec.Val2 + System.Environment.NewLine;
            fieldData += "  No Receipt Screen Number:         " + stRec.Val3 + System.Environment.NewLine;
            fieldData += "  Card Retain Screen Number:        " + stRec.Val4 + System.Environment.NewLine;
            fieldData += "  Statement Screen Number:          " + stRec.Val5 + System.Environment.NewLine;
            fieldData += "  Must Be 000:                      " + stRec.Val6 + System.Environment.NewLine;
            fieldData += "  BNA Notes Returned Screen Number: " + stRec.Val7 + System.Environment.NewLine;
            fieldData += "  Extension State Number:           " + stRec.Val8 + System.Environment.NewLine;
            fieldData += System.Environment.NewLine;

            return fieldData;

        }

    }
}
