using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class StateD : stateRec
    {
        public override void ValidateState(stateRec stateData)
        {

            Dictionary<string, stateRec> resultData = new Dictionary<string, stateRec>();

            base.ValidateState(stateData);

            if (stateData.stateType == "D")
            {

            }
        }
        public string getInfo(stateRec stRec)
        {
            string fieldData = "State Number: " + stRec.StateNumber + System.Environment.NewLine;
            fieldData += "  State Type: " + stRec.StateType + System.Environment.NewLine;
            fieldData += "  Next State Number: " + stRec.Val1 + System.Environment.NewLine;
            fieldData += "  Clear Mask: " + stRec.Val2 + System.Environment.NewLine;
            fieldData += "  'A' Preset Mask: " + stRec.Val3 + System.Environment.NewLine;
            fieldData += "  'A' Preset Mask: " + stRec.Val4 + System.Environment.NewLine;
            fieldData += "  'A' Preset Mask: " + stRec.Val5 + System.Environment.NewLine;
            fieldData += "  'A' Preset Mask: " + stRec.Val6 + System.Environment.NewLine;
            fieldData += "  Must Be 000: " + stRec.Val7 + System.Environment.NewLine;
            fieldData += "  Extension State Number: " + stRec.Val8 + System.Environment.NewLine;
            fieldData += System.Environment.NewLine;

            return fieldData;

        }

    }
}
