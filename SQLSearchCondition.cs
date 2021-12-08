using System;
using System.Data;

namespace Logger
{
    /// <summary>
    /// Manages the AdvancedFilter
    /// </summary>
    public class SQLSearchCondition
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
        /// Get the header info of all queries
        /// </summary>
        /// <returns>dt</returns>
        public DataTable getAllQueries(string sourceValue)
        {
            // get the query info
            string sql;

            // MLH Temporary remove selection to allow us to set and get all search conditions

            //if (sourceValue == "")
            sql = @"SELECT * FROM [sqlBuilder]";
            //else
            //    sql = @"SELECT * FROM [sqlBuilder] WHERE [source] = '" + sourceValue + "'" ;

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);

            return dt;
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
        public bool updateSearchCondition(SQLSearchCondition _searchCondition, string _queryName)
        {
            // get the builder ID
            DataTable dt = getQueryInfo(_queryName);

            if (dt == null) return false;

            if (dt.Rows.Count != 0)
            {
                string sql = @"UPDATE [sqlDetail] SET [fieldName] ='" + _searchCondition.SQLFieldName + "', " +
                "[condition] = '" + _searchCondition.SQLCondition + "', " +
                "[fieldValue] = '" + _searchCondition.SQLFieldValue + ", " +
                "[andOr] = '" + _searchCondition.SQLAndOr + "' " +
                "[fieldOutput] = '" + _searchCondition.SQLFieldOutput + "' " +
                "WHERE [queryID] ='" + dt.Rows[0]["sqlID"] + "'";

                DbCrud db = new DbCrud();
                return db.crudToDb(sql);
            }
            return false;
        }
        /// <summary>
        /// Delete the whole 6 lines of the SearchConditionBuilder and the header
        /// </summary>
        /// <param name="_sqlID"></param>
        /// <returns></returns>
        public bool deleteSearchConditionBuilder(int _sqlID)
        {
            // update the Builder
            string sql = @"DELETE FROM [sqlBuilder] WHERE [id] ='" + _sqlID + "'; " +
                          "DELETE FROM [sqlDetail] WHERE [sqlId] = '" + _sqlID + "'";

            DbCrud db = new DbCrud();
            return db.crudToDb(sql);
        }
        /// <summary>
        /// Update the content of the SearchConditionBuilder 
        /// </summary>
        /// <param name="_queryName"></param>
        /// <param name="_queryDescription"></param>
        /// <param name="_sqlID"></param>
        /// <returns></returns>
        public bool updateSearchConditionBuilder(string _queryName, string _queryDescription, int _sqlID)
        {
            // jmh
            // update the Builder
            string sourceValue = "U";  // User defined
            if (_queryDescription.Substring(0, 2) == "I-")
            {
                sourceValue = "I";   // Internal 
            }

            string sql = @"UPDATE [sqlBuilder] SET " +
                    " [name] = '" + _queryName + "'," +
                    " [description] = '" + _queryDescription + "'," +
                    " [date] = '" + DateTime.Now + "'," +
                    " [source] = '" + sourceValue + "' " +
                    "WHERE [id] ='" + _sqlID + "'; DELETE FROM [sqlDetail] WHERE [sqlId] = '" + _sqlID + "'";

            DbCrud db = new DbCrud();
            return db.crudToDb(sql);
        }
        /// <summary>
        /// Add-Create-Set a new AdvancedFilter header
        /// </summary>
        /// <param name="_queryName"></param>
        /// <param name="_queryDescription"></param>
        /// <returns>ID of the query just created</returns>
        public int setSearchConditionBuilder(string _queryName, string _queryDescription)
        {

            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();

            // insert the Builder

            string sourceValue = "U";  // User defined
            if (_queryDescription.Substring(0, 2) == "I-")
            {
                sourceValue = "I";   // Internal 
            }

            string sql = @"INSERT INTO [sqlBuilder]([name],[description],[date],[source]) " +
                   " VALUES('" + _queryName + "','" + _queryDescription + "','" + DateTime.Now + "','" + sourceValue + "'); SELECT CAST(scope_identity() AS int); ";

            return db.GetScalarIntFromDb(sql);
        }
        /// <summary>
        /// Set the content of the 6 lines of the AdvancedFilter
        /// 
        /// </summary>
        /// <param name="_searchCondition">Contains or holds one line of the AdvancedFilter</param>
        /// <param name="_sqlID">ID of the Query Name this is associated to</param>
        /// <returns></returns>
        public bool setSearchConditionDetail(SQLSearchCondition _searchCondition, int _sqlID)
        {
            // insert the Detail

            if (_searchCondition.sqlCondition == "" && _searchCondition.SQLFieldName == "[group8]")
            {
                _searchCondition.sqlCondition = "RegExp";
            }

            string sql = @"INSERT INTO [sqlDetail]([fieldName],[condition],[fieldValue],[andOr],[sqlID],[fieldOutput]) " +
                "VALUES('" + _searchCondition.SQLFieldName + "', '" + _searchCondition.SQLCondition + "', '" +
                             _searchCondition.SQLFieldValue + "', '" + _searchCondition.SQLAndOr + "', '" +
                             _sqlID + "', '" + _searchCondition.SQLFieldOutput + "')";

            DbCrud db = new DbCrud();
            return db.crudToDb(sql);
        }
    }
}
