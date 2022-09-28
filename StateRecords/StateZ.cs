using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    class StateZ : StateData
    {

        public override void ValidateState(StateData stateData)
        {
            Dictionary<string, StateData> resultData = new Dictionary<string, StateData>();
            this.ValidateState(stateData);

            if (stateData.StateType == "Z")
            {
                // validate General Purpose Buffer B or/and C
                // 000 - 001 - 002 - 003
                bool resp = this.ValidateRange(stateData.Val1, 0, 3);

                // validate optional datafield A to H (bit 1 thru 8)
                resp = this.ValidateRange(stateData.Val2, 0, 255);

                // validate optional datafield I to L (bit 1 thru 4)
                resp = this.ValidateRange(stateData.Val3, 0, 15);

                // validate optional datafield Q to V, w and a (bit 1 thru 8)
                resp = this.ValidateRange(stateData.Val4, 0, 255);


                // validate optional Data (bit 1 thru ?)
                resp = this.ValidateRange(stateData.Val5, 0, 31);

                // validate Must be zeros 
                if (!(stateData.Val6 == "000"))
                {
                    Console.WriteLine("Invalid value - Reserved ");
                }

                // EMV/CAM2 Processing Flag
                // 000 - 001 - 002 - 003
                resp = this.ValidateRange(stateData.Val7, 0, 3);

                // Extension State Number
                resultData = this.ValidateStateNumber(stateData.Val8);
            }
        }


        public override string checkZExtensions(StateData st)
        {
            // st holds the Z state

            string stateFound = "";
            foreach (StateData state in App.Prj.ExtensionsLst)
            {
                // state holds the state waiting for extension
                //if (st.StateNumber == state.StateNumber)
                if (state.StateType == "D")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        // this is the extension
                        stateFound = state.StateNumber + "D1";
                        break;
                    }
                }
                if (state.StateType == "J")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        // this is the extension
                        stateFound = state.StateNumber + "J1";
                        break;
                    }
                }
                if (state.StateType == "J1")
                {
                    if (state.Val4 == st.StateNumber)
                    {
                        stateFound = state.StateNumber + "J11";
                        break;
                    }
                }
                if (state.StateType == "J11")
                {
                    if (state.Val4 == st.StateNumber)
                    {
                        stateFound = state.StateNumber + "J111";
                        break;
                    }
                }
                if (state.StateType == "Y")
                {
                    if (state.Val5 == st.StateNumber)
                    {
                        // this is the extension
                        stateFound = state.StateNumber + "Y1";
                        break;
                    }
                    if (state.Val8 == st.StateNumber)
                    {
                        stateFound = state.StateNumber + "Y2";
                        break;
                    }
                }
                if (state.StateType == "I")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        stateFound = state.StateNumber + "I1";
                        break;
                    }
                }
                if (state.StateType == "I1")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        stateFound = state.StateNumber + "I11";
                        break;
                    }
                }
                if (state.StateType == ".")
                {
                    if (state.Val2 == st.StateNumber)
                    {
                        // this is the extension
                        stateFound = state.StateNumber + ".1";
                        break;
                    }
                    if (state.Val3 == st.StateNumber)
                    {
                        stateFound = state.StateNumber + ".2";
                        break;
                    }
                    if (state.Val4 == st.StateNumber)
                    {
                        stateFound = state.StateNumber + ".3";
                        break;
                    }

                }
                if (state.StateType == "/")
                {
                    if (state.Val4 == st.StateNumber)
                    {
                        stateFound = state.StateNumber + "/1";
                        break;
                    }
                }
                if (state.StateType == "&")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        stateFound = state.StateNumber + "&1";
                        break;
                    }
                }
                if (state.StateType == "&1")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        stateFound = state.StateNumber + "&11";
                        break;
                    }
                }
                if (state.StateType == ">")
                {
                    if (state.Val5 == st.StateNumber)
                    {
                        // this is the extension
                        stateFound = state.StateNumber + ">1";
                        break;
                    }
                    if (state.Val6 == st.StateNumber)
                    {
                        stateFound = state.StateNumber + ">2";
                        break;
                    }
                    if (state.Val7 == st.StateNumber)
                    {
                        stateFound = state.StateNumber + ">3";
                        break;
                    }
                }
                if (state.StateType == "T")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        // this is the extension
                        stateFound = state.StateNumber + "T1";
                        break;
                    }
                }
                if (state.StateType == "b")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        // this is the extension
                        stateFound = state.StateNumber + "b1";
                        break;
                    }
                }


                continue;

            }
            return stateFound;
        }

        public override void checkExtensions(StateData st)
        {
            bool stateExtension = false;

            // extension state is on field 6 (Val5)
            // language extension is on field 9 (Val8)

            DataTable dt = new DataTable();

            if ((st.StateType == "J1" && st.Val4 != "000" && st.Val5 != "255") ||
                (st.StateType == "J11" && st.Val4 != "000" && st.Val8 != "255") ||
                (st.StateType == "I1" && st.Val8 != "000" && st.Val8 != "255"))
            {
                stateExtension = true;
            }

            if (stateExtension)
                App.Prj.ExtensionsLst.Add(st);
        }
    }
}
