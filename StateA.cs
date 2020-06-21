using System;
using System.Collections.Generic;

namespace Logger
{
    class StateA : stateRec
    {
        public override void ValidateState(stateRec stateData)
        {

            Dictionary<string, stateRec> resultData = new Dictionary<string, stateRec>();

            base.ValidateState(stateData);

            // validate state data
            /*
             validate statNum is 000
             validate screen:
                exist in screen table
                is in the range of 000 - 999
             validate good read next state:
                exist in state table
                is 000-254 256-999
             validate misread screen:
                exist in screen table
                range is 000-999
             validate read conditions 1, 2, 3
             validate no fit match:
                exist in state table
                range is 000-254 256-999
             */
            if (stateData.stateType == "A")
            {
                if (stateData.stateNum != "000")
                {
                    Console.WriteLine("Erorr state type A not 000");
                }
                screenRec sr = new screenRec();
                // validate screen number with db
                bool resp = sr.ValidateScreen(stateData.sta1);

                // validate state number 
                resultData = this.ValidateStateNumber(stateData.sta2);

                // validate error screen
                resp = sr.ValidateScreen(stateData.sta3);

                // read conditions
                /*
                    bit    binary   decimal   meaning
                    0       0001      001       Read T3
                    1       0010      002       Read t2
                    1-0     0011      003       Read t2 and t3
                    2       0100      004       Read t1
                    2-0     0101      005       Read t1 and t3
                    2-1     0110      006       Read t1 and t2
                    2-1-0   0111      007       Read all tracks
                    3       1000      008       Chip connect 
                */

                // validate State type A Readcondition 1 between 1 and 7
                resp = this.ValidateRange(stateData.sta4, 1, 7);

                // validate State type A Readcondition 2 between 1 and 7
                resp = this.ValidateRange(stateData.sta5, 1, 7);

                // validate State type A Readcondition 3 between 1 and 8
                resp = this.ValidateRange(stateData.sta6, 1, 8);

                // validate State type A Card Return Flag between 0 and 1
                resp = this.ValidateRange(stateData.sta7, 0, 1);

                // validate state type A No FIT match state does not exist
                resultData = this.ValidateStateNumber(stateData.sta8);

            }
        }
        public string getInfo(stateRec stRec)
        {
            string fieldData = "State Number: " + stRec.StateNumber + System.Environment.NewLine;
            fieldData += "  State Type: " + stRec.StateType + System.Environment.NewLine;
            fieldData += "  Screen Number: " + stRec.Val1 + System.Environment.NewLine;
            fieldData += "  Good Read:" + stRec.Val2 + System.Environment.NewLine;
            fieldData += "  Missread: " + stRec.Val3 + System.Environment.NewLine;
            fieldData += "  Read Condition 1: " + stRec.Val4 + System.Environment.NewLine;
            fieldData += "  Read Condition 2: " + stRec.Val5 + System.Environment.NewLine;
            fieldData += "  Read Condition 3: " + stRec.Val6 + System.Environment.NewLine;
            fieldData += "  Card Return Flag: " + stRec.Val7 + System.Environment.NewLine;
            fieldData += "  No FIT Match: " + stRec.Val8 + System.Environment.NewLine;
            fieldData += System.Environment.NewLine;

            return fieldData;

        }

    }
}
