using System.Collections.Generic;

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
    }
}
