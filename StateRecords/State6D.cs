using System.Collections.Generic;

namespace Logger
{
    class State6D : StateData
    {
        public override void ValidateState(StateData stateData)
        {

            Dictionary<string, StateData> resultData = new Dictionary<string, StateData>();

            base.ValidateState(stateData);

            if (stateData.StateType == "m")
            {

            }
        }

        public override void checkExtensions(StateData st)
        {
            bool stateExtension = false;

            // extension state is on field 9(Val8) field 5(Val4) field 6(Val5)

            if ((st.Val4 != "000" && st.Val4 != "255") ||
                (st.Val5 != "000" && st.Val5 != "255") ||
                (st.Val8 != "000" && st.Val8 != "255"))

            {
                stateExtension = true;
            }

            if (stateExtension)
                App.Prj.ExtensionsLst.Add(st);
        }
    }
}
