﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    struct ejOptionsTimers
    {
        private string rectype;
        private string optionNumber;
        private string optionValue;
        private string optionNumber2;
        private string optionValue2;
        private string timerNumber;
        private string timerValue;

        public string Rectype { get => rectype; set => rectype = value; }
        public string OptionNumber { get => optionNumber; set => optionNumber = value; }
        public string OptionValue { get => optionValue; set => optionValue = value; }
        public string OptionNumber2 { get => optionNumber2; set => optionNumber2 = value; }
        public string OptionValue2 { get => optionValue2; set => optionValue2 = value; }
        public string TimerNumber { get => timerNumber; set => timerNumber = value; }
        public string TimerValue { get => timerValue; set => timerValue = value; }
    };

    class EjOptionsTimers : IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'M' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT TOP 1 * FROM ejOptionsTimers WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
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
                ejOptionsTimers eot = parseData(r.typeContent);

                string sql = @"INSERT INTO ejOptionsTimers([logkey],[rectype],[optionNumber],[optionValue],
                              [optionNumber2],[optionValue2],[timerNumber],[timerValue],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + eot.Rectype + "','" + eot.OptionNumber + "','" +
                              eot.OptionValue + "','" + eot.OptionNumber2 + "','" + eot.OptionValue2 + "','" +
                              eot.TimerNumber + "','" + eot.TimerValue + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public ejOptionsTimers parseData(string r)
        {
            ejOptionsTimers eot = new ejOptionsTimers();

            string[] tmpTypes = r.Split((char)0x1c);

            eot.Rectype = "M";

            if (tmpTypes[3].Length > 1)
            {
                eot.OptionNumber = tmpTypes[3].Substring(1, 2);
                eot.OptionValue = tmpTypes[3].Substring(3, 3);
            }

            if (tmpTypes[3].Length > 6)
            {
                eot.OptionNumber2 = tmpTypes[3].Substring(6, 2);
                eot.OptionValue2 = tmpTypes[3].Substring(8, 3);
            }

            if (tmpTypes[4].Length > 0)
            {
                eot.TimerNumber = tmpTypes[4].Substring(0, 2);
                eot.TimerValue = tmpTypes[4].Substring(2, 3);
            }
            return eot;
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