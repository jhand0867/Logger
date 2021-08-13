using System.Collections.Generic;

namespace Logger
{
    class State2D : StateData
    {
        public override void ValidateState(StateData stateData)
        {

            Dictionary<string, StateData> resultData = new Dictionary<string, StateData>();

            base.ValidateState(stateData);

            if (stateData.StateType == "-")
            {

            }
        }
    }
}
