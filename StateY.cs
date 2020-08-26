using System.Collections.Generic;
using System.Data;

namespace Logger
{
    // todo: remove Iextensions interface 
    class StateY : StateRec
    {
        public override void ValidateState(StateRec stateData)
        {

            Dictionary<string, StateRec> resultData = new Dictionary<string, StateRec>();

            base.ValidateState(stateData);

            if (stateData.stateType == "Y")
            {

            }
        }

        public override void checkExtensions(StateRec st)
        {
            bool stateExtension = false;

            // extension state is on field 6 (Val5)
            // language extension is on field 9 (Val8)

            DataTable dt = new DataTable();

            if (st.Val5 != "000" && st.Val5 != "255")
            {
                stateExtension = true;
            }
            if (st.Val8 != "000" && st.Val8 != "255")
            {
                stateExtension = true;
            }
            if (stateExtension)
                App.Prj.ExtensionsLst.Add(st);
        }
    }
}
