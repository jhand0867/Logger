﻿using System.Collections.Generic;

namespace Logger
{
    class StateM : StateRec
    {
        public override void ValidateState(StateRec stateData)
        {

            Dictionary<string, StateRec> resultData = new Dictionary<string, StateRec>();

            base.ValidateState(stateData);

            if (stateData.StateType == "M")
            {

            }
        }
    }
}