using System.Collections.Generic;

namespace Logger
{
    class State2F : StateData
    {
        public override void ValidateState(StateData stateData)
        {

            Dictionary<string, StateData> resultData = new Dictionary<string, StateData>();

            base.ValidateState(stateData);

            if (stateData.StateType == "/")
            {

            }
        }

        public override void checkExtensions(StateData st)
        {
            bool stateExtension = false;

            // extension state is on field 5 (Val4)

            if (st.Val4 != "000" || st.Val4 != "255")
            {
                stateExtension = true;
            }

            if (stateExtension)
                App.Prj.ExtensionsLst.Add(st);
        }
    }
}
