using System;
using System.Collections.Generic;

namespace Logger
{
    class StateI : StateRec
    {
        public override void ValidateState(StateRec stateData)
        {
            Dictionary<string, StateRec> resultData = new Dictionary<string, StateRec>();

            base.ValidateState(stateData);

            // validate state data
            /*
             validate statNum is 000 - 999
             validate screen:
                exist in screen table
                is in the range of 000 - 999
             validate good read next state:
                exist in state table
                is 000-254 256-999
             validate misread screen:
                exist in screen table
                range is 000-999
             validate read conditions 1, 2, 3
             validate no fit match:
                exist in state table
                range is 000-254 256-999
             */
            if (stateData.StateType == "I")
            {
                screenRec sr = new screenRec();
                // validate screen number with db
                bool resp = sr.ValidateScreen(stateData.Val1);

                // validate misread screen to go
                resultData = this.ValidateStateNumber(stateData.Val2);
                //Console.WriteLine("Error stat type A misread screen does not exist");

                // validate Track 2 is sent or not
                resp = this.ValidateYesNo(stateData.Val3);

                // validate Track 1 or 3 is between 0 and 7
                resp = this.ValidateRange(stateData.Val4, 0, 7);

                // validate OpCode Data
                resp = this.ValidateYesNo(stateData.Val5);

                // validate Amount Data
                resp = this.ValidateYesNo(stateData.Val6);
                bool extState = false;

                // validate PIN Buffer/Extended Format
                if (!(stateData.Val7 == "000" || stateData.Val7 == "001" ||
                    stateData.Val7 == "128" || stateData.Val7 == "129"))
                {
                    Console.WriteLine(" PIN Buffer/Extended Format value invalid ");
                }
                else if (stateData.Val7 == "128" || stateData.Val7 == "129")
                {
                    extState = true;
                }

                // validate General Purpose Buffer B or Extension State Number
                if (stateData.Val8 == "255")
                {
                    Console.WriteLine(" General Purpose Buffer B invalid ");
                }
                else
                {
                    if (stateData.Val8 == "000" || stateData.Val8 == "001")
                    {
                        resp = this.ValidateRange(stateData.Val8, 0, 7);
                    }
                    else
                    {
                        resp = this.ValidateRange(stateData.Val8, 0, 999);
                        resultData = this.ValidateStateNumber(stateData.Val8);
                    }
                }
            }
        }

        public override void checkExtensions(StateRec st)
        {
            bool stateExtension = false;

            // extension state is on field 9 (Val8)
            // Depeds on Val7 to be extension

            if (st.Val7 == "128" || st.Val7 == "129")
            {
                stateExtension = true;
            }

            if (stateExtension)
                App.Prj.ExtensionsLst.Add(st);
        }
        public override string checkZExtensions(StateRec st)
        {
            return null;
        }
    }
}

