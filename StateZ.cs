using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    class StateZ : StateRec
    {

        public override void ValidateState(StateRec stateData)
        {
            Dictionary<string, StateRec> resultData = new Dictionary<string, StateRec>();
            this.ValidateState(stateData);

            if (stateData.stateType == "Z")
            {
                // validate General Purpose Buffer B or/and C
                // 000 - 001 - 002 - 003
                bool resp = this.ValidateRange(stateData.sta1, 0, 3);

                // validate optional datafield A to H (bit 1 thru 8)
                resp = this.ValidateRange(stateData.sta2, 0, 255);

                // validate optional datafield I to L (bit 1 thru 4)
                resp = this.ValidateRange(stateData.sta3, 0, 15);

                // validate optional datafield Q to V, w and a (bit 1 thru 8)
                resp = this.ValidateRange(stateData.sta4, 0, 255);


                // validate optional Data (bit 1 thru ?)
                resp = this.ValidateRange(stateData.sta5, 0, 31);

                // validate Must be zeros 
                if (!(stateData.sta6 == "000"))
                {
                    Console.WriteLine("Invalid value - Reserved ");
                }

                // EMV/CAM2 Processing Flag
                // 000 - 001 - 002 - 003
                resp = this.ValidateRange(stateData.sta7, 0, 3);

                // Extension State Number
                resultData = this.ValidateStateNumber(stateData.sta8);
            }
        }

        public override string checkZExtensions(StateRec st)
        {
            // st holds the Z state

            string stateFound = "";
            foreach (StateRec state in App.Prj.ExtensionsLst)
            {
                // state holds the state waiting for extension
                //if (st.StateNumber == state.StateNumber)
                if (state.StateType == "D")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        // this is the extension
                        stateFound = state.stateNum + "D1";
                        break;
                    }
                }
                if (state.StateType == "J")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        // this is the extension
                        stateFound = state.stateNum + "J1";
                        break;
                    }
                }
                if (state.StateType == "J1")
                {
                    if (state.Val4 == st.StateNumber)
                    {
                        stateFound = state.stateNum + "J11";
                        break;
                    }
                }
                if (state.StateType == "J11")
                {
                    if (state.Val4 == st.StateNumber)
                    {
                        stateFound = state.stateNum + "J111";
                        break;
                    }
                }
                if (state.StateType == "Y")
                {
                    if (state.Val5 == st.StateNumber)
                    {
                        // this is the extension
                        stateFound = state.stateNum + "Y1";
                        break;
                    }
                    if (state.Val8 == st.StateNumber)
                    {
                        stateFound = state.stateNum + "Y2";
                        break;
                    }
                }
                if (state.StateType == "I")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        stateFound = state.stateNum + "I1";
                        break;
                    }
                }
                if (state.StateType == "I1")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        stateFound = state.stateNum + "I11";
                        break;
                    }
                }
                if (state.StateType == ".")
                {
                    if (state.Val2 == st.StateNumber)
                    {
                        // this is the extension
                        stateFound = state.stateNum + ".1";
                        break;
                    }
                    if (state.Val3 == st.StateNumber)
                    {
                        stateFound = state.stateNum + ".2";
                        break;
                    }
                    if (state.Val4 == st.StateNumber)
                    {
                        stateFound = state.stateNum + ".3";
                        break;
                    }

                }
                if (state.StateType == "/")
                {
                    if (state.Val4 == st.StateNumber)
                    {
                        stateFound = state.stateNum + "/1";
                        break;
                    }
                }
                if (state.StateType == "&")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        stateFound = state.stateNum + "&1";
                        break;
                    }
                }
                if (state.StateType == "&1")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        stateFound = state.stateNum + "&11";
                        break;
                    }
                }
                if (state.StateType == ">")
                {
                    if (state.Val5 == st.StateNumber)
                    {
                        // this is the extension
                        stateFound = state.stateNum + ">1";
                        break;
                    }
                    if (state.Val6 == st.StateNumber)
                    {
                        stateFound = state.stateNum + ">2";
                        break;
                    }
                    if (state.Val7 == st.StateNumber)
                    {
                        stateFound = state.stateNum + ">3";
                        break;
                    }
                }



                continue;

            }
            return stateFound;
        }
        public override void checkExtensions(StateRec st)
        {
            bool stateExtension = false;

            // extension state is on field 6 (Val5)
            // language extension is on field 9 (Val8)

            DataTable dt = new DataTable();

            if ((st.stateType == "J1" && st.Val4 != "000" && st.Val5 != "255") ||
                (st.stateType == "J11" && st.Val4 != "000" && st.Val8 != "255") ||
                (st.stateType == "I1" && st.Val8 != "000" && st.Val8 != "255"))
            {
                stateExtension = true;
            }

            if (stateExtension)
                App.Prj.ExtensionsLst.Add(st);
        }
    }
}
