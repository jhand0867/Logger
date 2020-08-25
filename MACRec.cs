using System;
using System.Collections.Generic;

namespace Logger
{
    class MACRec
    {

        // to be completed
        public void writeMAC(List<typeRec> typeRecs)
        {

            String sql = "";
            int loadNum = 0;
            foreach (typeRec r in typeRecs)
            {
                if (r.typeContent.Length < 10)
                {
                    continue;
                }

                loadNum++;


                sql = @"INSERT INTO DateTime([logkey],[rectype],[date],[time],[load])" +
                      " VALUES('" + r.typeIndex + "','" +
                                   '8' + "','" +
                                    r.typeContent.Substring(0, 6) + "','" + // date
                                    r.typeContent.Substring(6, 4) + "','" + // time
                                    loadNum.ToString() + "')";

                DbCrud db = new DbCrud();
                if (db.addToDb(sql) == false) { };

            }

        }

    }
}
