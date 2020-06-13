using System;
using System.Collections.Generic;

namespace Logger
{
    class StateZ : stateRec
    {

        public override void ValidateState(stateRec stateData)
        {
            Dictionary<string, stateRec> resultData = new Dictionary<string, stateRec>();
            this.ValidateState(stateData);

            if (stateData.stateType == "Z")
            {
                // validate General Purpose Buffer B or/and C
                // 000 - 001 - 002 - 003
                bool resp = this.ValidateRange(stateData.sta1, 0, 3);

                // validate optional datafield A to H (bit 1 thru 8)
                resp = this.ValidateRange(stateData.sta2, 0, 255);

                // validate optional datafield I to L (bit 1 thru 4)
                resp = this.ValidateRange(stateData.sta3, 0, 15);

                // validate optional datafield Q to V, w and a (bit 1 thru 8)
                resp = this.ValidateRange(stateData.sta4, 0, 255);


                // validate optional Data (bit 1 thru ?)
                resp = this.ValidateRange(stateData.sta5, 0, 31);

                // validate Must be zeros 
                if (!(stateData.sta6 == "000"))
                {
                    Console.WriteLine("Invalid value - Reserved ");
                }

                // EMV/CAM2 Processing Flag
                // 000 - 001 - 002 - 003
                resp = this.ValidateRange(stateData.sta7, 0, 3);

                // Extension State Number
                resultData = this.ValidateStateNumber(stateData.sta8);
            }
        }
    }
}
