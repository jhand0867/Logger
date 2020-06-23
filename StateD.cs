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
    }
}
