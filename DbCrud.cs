using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logger
{
    class DbCrud
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
         System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool crudToDb(string sql)
        {

            log.Info("CRUD to Database ");

            string connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                log.Debug("CRUD Request " + sql);
                command = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand.ExecuteNonQuery();
                command.Dispose();
                log.Debug("CRUD successful");

                

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

        public bool crudTransaction(string sql)
        {

            log.Info("CRUD to Database ");

            string connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            SqlConnection cnn = new SqlConnection(connectionString);

            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                log.Debug("CRUD Request " + sql);
                command = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand.ExecuteNonQuery();
                command.Dispose();
                log.Debug("CRUD successful");

                cnn.Close();
                return true;
            }

            catch (Exception dbEx)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
                log.Error("Database Error: " + dbEx.Message);
                return false;
            }

        }

        public DataTable GetTableFromDb(string sql)
        {
            log.Info("Read From Database ");

            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            DataTable dt = new DataTable();

            try
            {
                cnn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(sql, cnn))
                {
                    sda.Fill(dt);
                    sda.Dispose();
                }
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

            return dt;

        }

        public int GetScalarIntFromDb(string sql)
        {
            log.Info("Access Database and get scalar ");
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);

            try
            {
                cnn.Open();

                SqlCommand command;
                int result = 0;

                command = new SqlCommand(sql, cnn);

                result = (Int32)command.ExecuteScalar();

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

        //public SqlConnection GetScalarIntFromDbOpen()
        //{
        //    log.Info("Access Database and get scalar Open ");
        //    string connectionString;
        //    SqlConnection cnn;

        //    connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
        //    cnn = new SqlConnection(connectionString);
        //    cnn.Open();
        //    return cnn;
        //}
        //public void GetScalarIntFromDbClose(SqlConnection cnn)
        //{
        //    log.Info("Access Database and get scalar Close ");
        //    string connectionString;

        //    connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
        //    cnn.Close();
        //}

        //public int GetScalarIntFromDbNext(string sql, SqlConnection cnn)
        //{
        //    log.Info("Access Database and get scalar Next ");
        //    string connectionString;

        //    connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
        //    try
        //    {
        //        SqlCommand command;
        //        int result = 0;

        //        command = new SqlCommand(sql, cnn);

        //        result = (Int32)command.ExecuteScalar();

        //        command.Dispose();
        //        return result;
        //    }
        //    catch (Exception dbEx)
        //    {
        //        if (cnn.State == ConnectionState.Open)
        //        {
        //            cnn.Close();
        //        }
        //        log.Error("Database Error: " + dbEx.Message);
        //        return 0;
        //    }

        //}

        public string GetScalarStrFromDb(string sql)
        {
            log.Info("Access Database and get scalar ");
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);

            try
            {
                cnn.Open();

                SqlCommand command;
                string result;

                command = new SqlCommand(sql, cnn);

                result = (string)command.ExecuteScalar();

                command.Dispose();
                cnn.Close();
                log.Info($"Scalar returned '{result}'");
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
