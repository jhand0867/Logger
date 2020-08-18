namespace Logger
{
    class State26 : StateRec
    {
        public override void ValidateState(StateRec stateData)
        {

            if (stateData.stateType == "&")
            { }

        }

        public override void checkExtensions(StateRec st)
        {
            bool stateExtension = false;

            // extension state is on field 9 (Val8)
            // Depeds on Val7 to be extension

            if (st.Val8 == "000" || st.Val8 == "255")
            {
                stateExtension = true;
            }

            if (stateExtension)
                App.Prj.ExtensionsLst.Add(st);
        }
    }
}

