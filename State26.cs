using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logger
{
    class State26 : stateRec
    {
        public override void ValidateState(stateRec stateData)
        {

            if (stateData.stateType == "&")
            { }
                
        }

        public override void checkExtensions(stateRec st)
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

