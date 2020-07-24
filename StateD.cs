using System.Collections.Generic;

namespace Logger
{
    class StateD : stateRec
    {
        public override void ValidateState(stateRec stateData)
        {

            Dictionary<string, stateRec> resultData = new Dictionary<string, stateRec>();

            base.ValidateState(stateData);

            if (stateData.stateType == "D")
            {

            }
        }

        public override void checkExtensions(stateRec st)
        {
            bool stateExtension = false;

            // extension state is on field 9 (Val8)

            if (st.Val8 != "000" && st.Val8 != "255")
            {
                stateExtension = true;
            }

            if (stateExtension)
                App.Prj.ExtensionsLst.Add(st);
        }
    }
}
