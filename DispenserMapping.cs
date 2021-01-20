using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    struct dispenserMapping
    {
        private string rectype;
        private string numberMappingEntries;
        private string mappingTable;
        //private string currencyType;
        //private string cassetteType;
        //private string denomination;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string NumberMappingEntries { get => numberMappingEntries; set => numberMappingEntries = value; }
        public string MappingTable { get => mappingTable; set => mappingTable = value; }
        //public string CurrencyType { get => currencyType; set => currencyType = value; }
        //public string CassetteType { get => cassetteType; set => cassetteType = value; }
        //public string Denomination { get => denomination; set => denomination = value; }
        public string Mac { get => mac; set => mac = value; }
    };

    class DispenserMapping : IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'G' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT TOP 1 * FROM dispenserMapping WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
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
            {
                for (int colNum = 3; colNum < dts[0].Columns.Count - 2; colNum++)
                {
                    txtField += getOptionDescription(ss, colNum.ToString("00"), dts[0].Rows[0][colNum].ToString());
                    txtField += "\t" + System.Environment.NewLine;

                }
            }
            return txtField;
        }

        public bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                dispenserMapping dm = parseData(r.typeContent);

                string sql = @"INSERT INTO dispenserMapping([logkey],[rectype],[numberMappingEntries],
                                        	[mappingTable],[mac],[prjkey],[logID])" +
                            " VALUES('" + r.typeIndex + "','" + dm.Rectype + "','" + dm.NumberMappingEntries + "','" +
                              //   dm.CurrencyType + "','" + dm.CassetteType + "','" + dm.Denomination + "','" +
                              dm.MappingTable + "','" + dm.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public dispenserMapping parseData(string r)
        {
            dispenserMapping dm = new dispenserMapping();

            string[] tmpTypes = r.Split((char)0x1c);

            dm.Rectype = "G";
            dm.NumberMappingEntries = tmpTypes[4].Substring(0,2);
            dm.MappingTable = tmpTypes[4].Substring(2, tmpTypes[4].Length - 2);

            if (tmpTypes.Length > 5)
                dm.Mac = tmpTypes[5];

            return dm;
        }

        internal string getOptionDescription(DataTable dataTable, string field, string fieldValue)
        {

            // todo: enter data descriptions for all records
            // todo: put together the digesting routines for all record types

            string optionDesc = "";
            string fieldDesc = "";

            // what's the description of the field
            foreach (DataRow item in dataTable.Rows)
            {
                if (item[2].ToString().Trim() == field)
                {
                    optionDesc = item[3].ToString().Trim();

                    if (item[5].ToString() != null && item[5].ToString() != "")
                    {
                        Digester myDigester = MessageFactory.Create_Digester();
                        fieldDesc = myDigester.fieldDigester(item[5].ToString(), fieldValue);
                        fieldValue = fieldValue.Replace(";", " ");
                    }
                    optionDesc += " = " + fieldValue + insertDescription(item[4].ToString()) + fieldDesc;

                    break;
                }
            }
            return optionDesc;
        }

        private string insertDescription(string fieldDescription)
        {
            string description = "";

            if (fieldDescription != "")
            {
                if (fieldDescription.Contains("\r\n"))
                {
                    description += System.Environment.NewLine + fieldDescription.Trim() + System.Environment.NewLine;
                }
                else
                {
                    description += "\t" + fieldDescription.Trim() + System.Environment.NewLine;
                }
            }
            else
            {
                description += fieldDescription.Trim();
            }
            return description;
        }
    }
}

