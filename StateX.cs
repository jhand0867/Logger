using System.Collections.Generic;

namespace Logger
{
    class StateX : StateRec
    {
        public void ValidateState(StateRec stateData)
        {

            Dictionary<string, StateRec> resultData = new Dictionary<string, StateRec>();

            base.ValidateState(stateData);

            if (stateData.stateType == "X")
            {

            }
        }

        public void checkExtensions(StateRec st)
        {
            bool stateExtension = false;

            // extension state is on field 9 (Val8)

            if (st.Val5 != "000" && st.Val5 != "255")
            {
                stateExtension = true;
            }

            if (stateExtension)
                App.Prj.ExtensionsLst.Add(st);
        }
    }
}
