﻿using System.Collections.Generic;

namespace Logger
{
    class State6B : StateData
    {
        public override void ValidateState(StateData stateData)
        {

            Dictionary<string, StateData> resultData = new Dictionary<string, StateData>();

            base.ValidateState(stateData);

            if (stateData.StateType == "k")
            {

            }
        }
    }
}
