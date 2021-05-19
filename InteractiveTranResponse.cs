using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct interactiveTranRsp
    {
        private string rectype;
        private string displayFlag;
        private string activeKeys;
        private string screenTimerField;
        private string screenDataField;

        public string Rectype { get => rectype; set => rectype = value; }
        public string DisplayFlag { get => displayFlag; set => displayFlag = value; }
        public string ActiveKeys { get => activeKeys; set => activeKeys = value; }
        public string ScreenTimerField { get => screenTimerField; set => screenTimerField = value; }
        public string ScreenDataField { get => screenDataField; set => screenDataField = value; }
    };

    class InteractiveTranResponse : IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'Q' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT TOP 1 * FROM interactiveTranResponse WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
                                               "' AND logkey LIKE '" + logKey + "%'";
            DataTable dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable ss = getDescription();

            if (dts[0].Rows.Count > 0)
                for (int colNum = 3; colNum < dts[0].Columns.Count - 2; colNum++)
                    txtField += App.Prj.getOptionDescription(ss, colNum.ToString("00"), dts[0].Rows[0][colNum].ToString());

            return txtField;
        }

        public bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                interactiveTranRsp itr = parseData(r.typeContent);

                string sql = @"INSERT INTO interactiveTranResponse([logkey],[rectype],[displayFlag],
	                                    [activeKeys],[screenTimerField],[screenDataField],[prjkey],[logID])" +
                            " VALUES('" + r.typeIndex + "','" + itr.Rectype + "','" + itr.DisplayFlag + "','" +
                              itr.ActiveKeys + "','" + itr.ScreenTimerField + "','" + itr.ScreenDataField + "','" +
                              Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public interactiveTranRsp parseData(string r)
        {
            interactiveTranRsp itr = new interactiveTranRsp();

            string[] tmpTypes = r.Split((char)0x1c);

            itr.Rectype = "Q";
            itr.DisplayFlag = tmpTypes[3].Substring(1, 1);
            itr.ActiveKeys = tmpTypes[3].Substring(2, tmpTypes[3].Length - 2);
            itr.ScreenTimerField = tmpTypes[4];
            itr.ScreenDataField = tmpTypes[5];

            return itr;
        }

        //internal string getOptionDescription(DataTable dataTable, string field, string fieldValue)
        //{

        //    // todo: enter data descriptions for all records
        //    // todo: put together the digesting routines for all record types

        //    string optionDesc = "";
        //    string fieldDesc = "";

        //    // what's the description of the field
        //    foreach (DataRow item in dataTable.Rows)
        //    {
        //        if (item[2].ToString().Trim() == field)
        //        {
        //            optionDesc = item[3].ToString().Trim();

        //            if (item[5].ToString() != null && item[5].ToString() != "")
        //            {
        //                Digester myDigester = LoggerFactory.Create_Digester();
        //                fieldDesc = myDigester.fieldDigester(item[5].ToString(), fieldValue);
        //            }
        //            optionDesc += " = " + fieldValue + insertDescription(item[4].ToString()) + fieldDesc;

        //            break;
        //        }
        //    }
        //    return optionDesc;
        //}

        //private string insertDescription(string fieldDescription)
        //{
        //    string description = "";

        //    if (fieldDescription != "")
        //    {
        //        if (fieldDescription.Contains("\r\n"))
        //        {
        //            description += System.Environment.NewLine + fieldDescription.Trim() + System.Environment.NewLine;
        //        }
        //        else
        //        {
        //            description += "\t" + fieldDescription.Trim() + System.Environment.NewLine;
        //        }
        //    }
        //    else
        //    {
        //        description += fieldDescription.Trim();
        //    }
        //    return description;
        //}
    }
}

