using System.Collections.Generic;
using System.Data;

namespace Logger
{
    class State77 : StateData
    {
        public override void ValidateState(StateData stateData)
        {

            Dictionary<string, StateData> resultData = new Dictionary<string, StateData>();

            base.ValidateState(stateData);

            if (stateData.StateType == "Y")
            {

            }
        }

        public override void checkExtensions(StateData st)
        {
            bool stateExtension = false;

            // extension state is on field 6 (Val5)
            // language extension is on field 7 (Val6)

            DataTable dt = new DataTable();

            if (st.Val5 != "000" && st.Val5 != "255")
            {
                stateExtension = true;
            }
            if (st.Val6 != "000" && st.Val6 != "255")
            {
                stateExtension = true;
            }
            if (stateExtension)
                App.Prj.ExtensionsLst.Add(st);
        }
    }
}
