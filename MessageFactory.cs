using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public static class MessageFactory
    {
        public static Project Create_Project()
        {
            return new Project();
        }

        public static List<typeRec> Create_ListOfTypeRec()
        {
            return new List<typeRec>();
        }


    }
}
