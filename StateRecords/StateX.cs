using System.Collections.Generic;

namespace Logger
{
    class StateX : StateData
    {
        public override void ValidateState(StateData stateData)
        {

            Dictionary<string, StateData> resultData = new Dictionary<string, StateData>();

            base.ValidateState(stateData);

            if (stateData.StateType == "X")
            {

            }
        }

        public override void checkExtensions(StateData st)
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
