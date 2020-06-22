using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class StateB : stateRec
    {
        public override void ValidateState(stateRec stateData)
        {

            Dictionary<string, stateRec> resultData = new Dictionary<string, stateRec>();

            base.ValidateState(stateData);

            if (stateData.stateType == "B")
            {

            }
        }
        public string getInfo(stateRec stRec)
        {
            string fieldData = "State Number: " + stRec.StateNumber + System.Environment.NewLine;
            fieldData += "  State Type: " + stRec.StateType + System.Environment.NewLine;
            fieldData += "  Screen Number: " + stRec.Val1 + System.Environment.NewLine;
            fieldData += "  Time Out State Number: " + stRec.Val2 + System.Environment.NewLine;
            fieldData += "  Cancel State Number: " + stRec.Val3 + System.Environment.NewLine;
            fieldData += "  Local PIN Check Good State Number: " + stRec.Val4 + System.Environment.NewLine;
            fieldData += "  Local PIN Check Maximun Bad State Number: " + stRec.Val5 + System.Environment.NewLine;
            fieldData += "  Local PIN Check Error Screen Number: " + stRec.Val6 + System.Environment.NewLine;
            fieldData += "  Remote PIN Check State Number: " + stRec.Val7 + System.Environment.NewLine;
            fieldData += "  Local PIN Check Maximun PIN Retries: " + stRec.Val8 + System.Environment.NewLine;
            fieldData += System.Environment.NewLine;

            return fieldData;

        }

    }
}
