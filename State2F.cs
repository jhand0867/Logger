using System.Collections.Generic;

namespace Logger
{
    class State2F : stateRec
    {
        public override void ValidateState(stateRec stateData)
        {

            Dictionary<string, stateRec> resultData = new Dictionary<string, stateRec>();

            base.ValidateState(stateData);

            if (stateData.stateType == "/")
            {

            }
        }

        public override void checkExtensions(stateRec st)
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
