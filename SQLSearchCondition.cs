using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class SQLSearchCondition
    {
        /// <summary>
        /// Represents a row from the advanced search grid
        /// holds strings for 
        /// </summary>
        private string sqlFieldName;
        private string sqlCondition;
        private string sqlFieldValue;
        private string sqlAndOr;
        private string sqlFieldOutput;

        public string SQLFieldName { get => sqlFieldName; set => sqlFieldName = value; }
        public string SQLCondition { get => sqlCondition; set => sqlCondition = value; }
        public string SQLFieldValue { get => sqlFieldValue; set => this.sqlFieldValue = value; }
        public string SQLAndOr { get => sqlAndOr; set => sqlAndOr = value; }
        public string SQLFieldOutput { get => sqlFieldOutput; set => sqlFieldOutput = value; }

        public SQLSearchCondition()
        {
            SQLAndOr = "";
            SQLFieldName = "";
            SQLCondition = "";
            SQLFieldValue = "";
            SQLFieldOutput = "";
        }

        public SQLSearchCondition(string _andOr, string _fieldName, string _condition, string _value, string _output)
        {
            SQLAndOr = _andOr; 
            SQLFieldName = _fieldName;
            SQLCondition = _condition;
            SQLFieldValue = _value;
            SQLFieldOutput = _output;
        }

        /// <summary>
        /// Get the header info of the query by name
        /// </summary>
        /// <param name="_queryName"></param>
        /// <returns>dt</returns>
        public DataTable getQueryInfo(string _queryName)
        {
            // get the query info
            string sql = @"SELECT * FROM [sqlBuilder] WHERE [name] = '" + _queryName + "'";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            
            return dt;
        }
        /// <summary>
        /// Gets all the related 6 lines of AdvancedFilter
        /// </summary>
        /// <param name="_queryName"></param>
        /// <returns>dt</returns>
        public DataTable getSearchCondition(string _queryName)
        {
            DataTable dt = new DataTable();
            if (_queryName != "")
            {
                // get the query info
                dt = getQueryInfo(_queryName);

                if (dt.Rows.Count != 0)
                {
                    string sql = @"SELECT * FROM [sqlDetail] WHERE [sqlId] = '" + dt.Rows[0]["Id"] + "'";

                    DbCrud db = new DbCrud();
                    dt = db.GetTableFromDb(sql);
                }
            }
            return dt;
        }


        /// <summary>
        /// Need to get the query name first to be able 
        /// to update the whole condition
        /// </summary>
        /// <param name="_searchCondition"></param>
        /// <param name="_queryName"></param>
        /// 
        public void updateSearchCondition(SQLSearchCondition _searchCondition, string _queryName)
        {
            // get the builder ID
            DataTable dt = getQueryInfo(_queryName);

            string sql = @"UPDATE [sqlDetail] SET [fieldName] ='" + _searchCondition.SQLFieldName + "', " +
                "[condition] = '" + _searchCondition.SQLCondition + "', " +
                "[fieldValue] = '" + _searchCondition.SQLFieldValue + ", " +
                "[andOr] = '" + _searchCondition.SQLAndOr + "' " +
                "WHERE [queryID] ='" + dt.Rows[0]["sqlID"] + "'";

            DbCrud db = new DbCrud();
            db.crudToDb(sql);
        }

        public int setSearchConditionBuilder(string _queryName, string _queryDescription)
        {

            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();

            // insert the Builder
            string sql = @"INSERT INTO [sqlBuilder]([name],[description],[date]) " +
                   " VALUES('" + _queryName + "','" + _queryDescription + "','" + DateTime.Now + "'); SELECT CAST(scope_identity() AS int); ";

            //string sql = @"INSERT INTO logs(prjKey, logFile, uploadDate) 
            //        VALUES('" + pKey + "','" + pFilename + "', GETDATE()); SELECT CAST(scope_identity() AS int);";


            return db.GetScalarIntFromDb(sql);
        }

        public bool setSearchConditionDetail(SQLSearchCondition _searchCondition, int _sqlID)
        {
            // insert the Detail
            string sql = @"INSERT INTO [sqlDetail]([fieldName],[condition],[fieldValue],[andOr],[sqlID]) " +
                "VALUES('" + _searchCondition.SQLFieldName + "', '" + _searchCondition.SQLCondition + "', '" +
                             _searchCondition.SQLFieldValue + "', '" +  _searchCondition.SQLAndOr + "', '" +
                             _sqlID + "')";

            DbCrud db = new DbCrud();
            return db.crudToDb(sql);
            
        }
    }
}
