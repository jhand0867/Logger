using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    class StateZ : stateRec
    {

        public override void ValidateState(stateRec stateData)
        {
            Dictionary<string, stateRec> resultData = new Dictionary<string, stateRec>();
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

        public new DataTable checkZExtensions(stateRec st)
        {
            // st holds the Z state
            DataTable dt = new DataTable();
            foreach (stateRec state in App.Prj.ExtensionsLst)
            {
                // state holds the state waiting for extension
                //if (st.StateNumber == state.StateNumber)

                if (state.StateType == "J")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        // this is the extension
                        dt = st.getStateDescription("J1");
                        break;
                    }
                 }
                if (state.StateType == "J1")
                {
                    if (state.Val4 == st.StateNumber)
                    {
                        dt = st.getStateDescription("J11");
                        break;
                    }
                }
                if (state.StateType == "J11")
                {
                    if (state.Val4 == st.StateNumber)
                    {
                        dt = st.getStateDescription("J111");
                        break;
                    }
                }
                if (state.StateType == "Y")
                {
                    if (state.Val5 == st.StateNumber)
                    {
                        // this is the extension
                        dt = st.getStateDescription("Y1");
                        break;
                    }
                    if (state.Val8 == st.StateNumber)
                    {
                        dt = st.getStateDescription("Y2");
                        break;
                    }
                }
                if (state.StateType == "I")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        dt = st.getStateDescription("I1");
                        break;
                    }
                }
                if (state.StateType == "I1")
                {
                    if (state.Val8 == st.StateNumber)
                    {
                        dt = st.getStateDescription("I11");
                        break;
                    }
                }

                continue;

            }
            return dt;
        }

        public override void checkExtensions(stateRec st)
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

        /*        public override DataTable checkExtensions(stateRec st)
                {
                    DataTable dt = new DataTable();
                    List<stateRec> stw = App.Prj.ExtensionsLst;
                    for (int x=stw.Count; x>0; x--)
                    {
                        if (stw[x-1].StateNumber == st.StateNumber)
                        {

                        } 

                    }

                    return dt;
                }
        */
    }
}
