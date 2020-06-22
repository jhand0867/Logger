using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class StateK : stateRec
    {
        public override void ValidateState(stateRec stateData)
        {

           Dictionary<string, stateRec> resultData = new Dictionary<string, stateRec>();

           base.ValidateState(stateData);

           if (stateData.stateType == "K")
            {
   
            }
        }
        public string getInfo(stateRec stRec)
        {
            string fieldData = "State Number: " + stRec.StateNumber + System.Environment.NewLine;
            fieldData += "  State Type: " + stRec.StateType + System.Environment.NewLine;
            fieldData += "  Next State Number: " + stRec.Val1 + System.Environment.NewLine;
            fieldData += "  Next State Number: " + stRec.Val2 + System.Environment.NewLine;
            fieldData += "  Next State Number: " + stRec.Val3 + System.Environment.NewLine;
            fieldData += "  Next State Number: " + stRec.Val4 + System.Environment.NewLine;
            fieldData += "  Next State Number: " + stRec.Val5 + System.Environment.NewLine;
            fieldData += "  Next State Number: " + stRec.Val6 + System.Environment.NewLine;
            fieldData += "  Next State Number: " + stRec.Val7 + System.Environment.NewLine;
            fieldData += "  Next State Number: " + stRec.Val8 + System.Environment.NewLine;
            fieldData += System.Environment.NewLine;

            return fieldData;

        }

    }
}
