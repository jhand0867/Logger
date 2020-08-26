using System.Collections.Generic;

namespace Logger
{
    class State2E : StateRec
    {
        public override void ValidateState(StateRec stateData)
        {

            Dictionary<string, StateRec> resultData = new Dictionary<string, StateRec>();

            base.ValidateState(stateData);

            if (stateData.stateType == ".")
            {

            }
        }
        public override void checkExtensions(StateRec st)
        {
            bool stateExtension = false;

            // extension state is on field 3,4,5 (Val2,3,4)

            if (st.Val2 != "000" || st.Val2 != "255" ||
                st.Val3 != "000" || st.Val3 != "255" ||
                st.Val4 != "000" || st.Val4 != "255")
            {
                stateExtension = true;
            }

            if (stateExtension)
                App.Prj.ExtensionsLst.Add(st);
        }
    }
}

