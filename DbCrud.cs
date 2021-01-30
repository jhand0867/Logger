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
                log.Error("Database Error: " + dbEx.Message);
                return false;
            }

        }

        public bool crudTransaction(string sql)
        {

            log.Info("CRUD to Database ");

            string connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            SqlConnection cnn = new SqlConnection(connectionString);
            SqlTransaction transaction;

            transaction = cnn.BeginTransaction();
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
                }
            }
            catch (Exception dbEx)
            {
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
                int result;

                command = new SqlCommand(sql, cnn);

                result = (Int32)command.ExecuteScalar();

                command.Dispose();
                cnn.Close();
                return result;
            }
            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return 0;
            }

        }

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
                log.Error("Database Error: " + dbEx.Message);
                return null;
            }

        }

    }
}
