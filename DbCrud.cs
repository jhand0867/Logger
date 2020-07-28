using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Logger
{
    class DbCrud
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
         System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool addToDb(string sql)
        {
            log.Info("Adding to Database ");

            string connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            SqlConnection cnn = new SqlConnection(connectionString);

            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                log.Debug("Adding record " + sql);
                command = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand.ExecuteNonQuery();
                command.Dispose();
                log.Debug("Record Added");

                cnn.Close();
                return true;
            }

            catch (Exception dbEx)
            {
                log.Error("Database Error: " + dbEx.Message);
                return false;
            }

        }
    }
}
