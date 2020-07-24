using System.Collections.Generic;

namespace Logger
{
    class StateX : stateRec
    {
        public override void ValidateState(stateRec stateData)
        {

            Dictionary<string, stateRec> resultData = new Dictionary<string, stateRec>();

            base.ValidateState(stateData);

            if (stateData.stateType == "X")
            {

            }
        }

        public override void checkExtensions(stateRec st)
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
