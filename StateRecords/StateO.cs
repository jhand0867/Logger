﻿using System.Collections.Generic;

namespace Logger
{
    class StateO : StateData
    {
        public override void ValidateState(StateData stateData)
        {

            Dictionary<string, StateData> resultData = new Dictionary<string, StateData>();

            base.ValidateState(stateData);

            if (stateData.StateType == "O")
            {

            }
        }
    }
}
