using System.Collections.Generic;

namespace Logger
{
    class StateJ : StateRec
    {
        // todo: add override to all state classes

        public void ValidateState(StateRec stateData)
        {

            Dictionary<string, StateRec> resultData = new Dictionary<string, StateRec>();

            base.ValidateState(stateData);

            if (stateData.stateType == "J")
            {

            }
        }

        public void checkExtensions(StateRec st)
        {
            bool stateExtension = false;

            // extension state is on field 6 (Val5)
            // language extension is on field 9 (Val8)

            if (st.Val8 != "000" && st.Val8 != "255")
            {
                stateExtension = true;
            }
            if (stateExtension)
                App.Prj.ExtensionsLst.Add(st);
        }
    }
}
