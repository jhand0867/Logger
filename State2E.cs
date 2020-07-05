using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class State2E : stateRec
    {
        public override void ValidateState(stateRec stateData)
        {

            Dictionary<string, stateRec> resultData = new Dictionary<string, stateRec>();

            base.ValidateState(stateData);

            if (stateData.stateType == ".")
            {

            }
        }
        public override void checkExtensions(stateRec st)
        {
            bool stateExtension = false;

            // extension state is on field 3,4,5 (Val2,3,4)

            if (st.Val2 != "000" || st.Val2 != "255" ||
                st.Val3 != "000" || st.Val3 != "255" ||
                st.Val4 != "000" || st.Val4 != "255")
            {
                stateExtension = true;
            }

            if (stateExtension)
                App.Prj.ExtensionsLst.Add(st);
        }
    }
}

