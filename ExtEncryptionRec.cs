﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Logger
{
    class ExtEncryptionRec : App
    {
        public bool writeData(List<typeRec> typeRecs, string key, string logID)
        {
                String sql = "";
                int loadNum = 0;
                foreach (typeRec r in typeRecs)
                {
                    if (r.typeContent.Length < 3)
                    {
                        continue;
                    }

                    loadNum++;


                    sql = @"INSERT INTO extEncryption([logkey],[rectype],[keySize],[keyData],[load],[prjkey],[logID])" +
                          " VALUES('" + r.typeIndex + "','" +
                                       'X' + "','" +
                                        r.typeContent.Substring(0, 3) + "','" + // key data size
                                        r.typeContent.Substring(3, r.typeContent.Length - 3) + "','" + // Key Data
                                        loadNum.ToString() + "','" + key + "'," + logID + ")";

                    DbCrud db = new DbCrud();
                    if (db.addToDb(sql) == false)
                        return false;
                }
                return true;
        }

    }
}
