using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Threading.Tasks;
//using System.Data.SqlClient;
//using Devart.Data.SQLite;

namespace Logger
{
    class DbCrud
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
         System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool crudToDb(string sql)
        {
            string dbConnString = ConfigurationManager.ConnectionStrings["LoggerSQLite"].ConnectionString;
            OdbcConnection DbConnection = new OdbcConnection(dbConnString);

            log.Info("CRUD to Database ");

            string connectionString = ConfigurationManager.ConnectionStrings["LoggerSQLite"].ConnectionString;
            OdbcConnection cnn = new OdbcConnection(connectionString);
            log.Debug("CRUD Request " + sql);

            try
            {
                cnn.Open();

                OdbcCommand command = cnn.CreateCommand();
                command.CommandText = sql;
                int i = command.ExecuteNonQuery();
                command.Dispose();
                log.Info("CRUD successful");
                cnn.Close();
                return true;
            }

            catch (Exception dbEx)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                int currentLine = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber();

                log.Error("Database Error: " + dbEx.Message + " " + currentFile + "(" + currentLine.ToString() + ")");
                return false;
            }

        }

        public async Task<DataTable> GetTableFromDbAsync(string sql)
        {
            log.Info("Reading from database ");
            log.Debug($"Read From Database: {sql}  ");

            string connectionString;
            OdbcConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerSQLite"].ConnectionString;
            cnn = new OdbcConnection(connectionString);
            DataTable dt = new DataTable();
            var tempDt = dt;

            try
            {
                cnn.Open();
                OdbcDataAdapter sda = new OdbcDataAdapter(sql, cnn);
                await Task.Run(() => sda.Fill(tempDt));
                cnn.Close();
            }
            catch (Exception dbEx)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
                log.Error("Database Error: " + dbEx.ToString());
                return null;
            }
            dt = tempDt;
            return dt;

        }



        public DataTable GetTableFromDb(string sql)
        {
            log.Info("Getting a table from database ");
            log.Debug($"Getting a table from database: {sql}  ");

            string connectionString;
            OdbcConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerSQLite"].ConnectionString;
            cnn = new OdbcConnection(connectionString);
            DataTable dt = new DataTable();
            var tempDt = dt;

            try
            {
                cnn.Open();
                OdbcDataAdapter sda = new OdbcDataAdapter(sql, cnn);
                sda.Fill(tempDt);
                cnn.Close();
            }
            catch (Exception dbEx)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
                log.Error("Database Error: " + dbEx.ToString());
                return null;
            }
            dt= tempDt;
            return dt;

        }



        public int GetScalarIntFromDb(string sql)
        {
            log.Info("Getting int from database ");
            log.Debug($"Access Database and get scalar: {sql}  ");

            string connectionString;
            OdbcConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerSQLite"].ConnectionString;
            cnn = new OdbcConnection(connectionString);

            try
            {
                cnn.Open();

                OdbcCommand command = cnn.CreateCommand();
                command.CommandText = sql;
                object o = command.ExecuteScalar();

                int result = 0;
                result = (Int32)o;

                command.Dispose();
                cnn.Close();
                return result;
            }
            catch (Exception dbEx)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
                log.Error("Database Error: " + dbEx.Message);
                return 0;
            }

        }

        public string GetScalarStrFromDb(string sql)
        {
            log.Info("Getting string from database ");
            log.Debug($"Get count of: {sql} ");
            string connectionString;
            OdbcConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerSQLite"].ConnectionString;
            cnn = new OdbcConnection(connectionString);

            try
            {
                cnn.Open();

                OdbcCommand command = cnn.CreateCommand();
                command.CommandText = sql;
                object o = command.ExecuteScalar();

                string result = null;
                if (o != null)
                    result = o.ToString();

                command.Dispose();
                cnn.Close();
                return result;
            }
            catch (Exception dbEx)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
                log.Error("Database Error: " + dbEx.Message);
                return null;
            }
        }
    }
}
