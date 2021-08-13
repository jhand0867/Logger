namespace Logger
{
    class State26 : StateData
    {
        public override void ValidateState(StateData stateData)
        {

            if (stateData.StateType == "&")
            { }

        }

        // public override void checkExtensions(StateRec st)
        public override void checkExtensions(StateData st)
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

