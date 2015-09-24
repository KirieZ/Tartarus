//Written for Tartarus by iSmokeDrow
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Common
{
    /// <summary>
    /// Provides interactibility with MySQL/MSSQL databases
    /// </summary>
	public class DBManager : IDisposable
	{
        /// <summary>
        /// Indentifies the current connection type
        /// 0 = MSSQL
        /// 1 = MySQL
        /// </summary>
        readonly int conType = 0;
        internal SqlConnection mssqlConnection;
        internal MySqlConnection mysqlConnection;

        public DBManager(int type, string conString)
        {
            conType = type;
            string consoleMsg = "Initializing Database Instance for";

            switch (type)
            {
                case 0: //MSSQL
                    mssqlConnection = new SqlConnection(conString);
                    consoleMsg += " MSSQL";
                    break;

                case 1: //MySQL
                    mysqlConnection = new MySqlConnection(conString);
                    consoleMsg += " MySQL";
                    break;
            }

            ConsoleUtils.ShowDebug(consoleMsg);
        }

        /// <summary>
        /// Opens an SQL Connection of the appropriate type and returns it' status
        /// </summary>
        public bool Open
        {
            get
            {
                try
                {
                    bool conOpen = false;

                    switch (conType)
                    {
                        case 0:
                            mssqlConnection.Open();
                            conOpen = mssqlConnection.State == ConnectionState.Open;
                            break;

                        case 1:
                            mysqlConnection.Open();
                            conOpen = mysqlConnection.State == ConnectionState.Open;
                            break;
                    }

                    return conOpen;
                }
                catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); return false; }
            }
        }

        #region Execution Methods

        /// <summary>
        /// Executes a database read operation
        /// </summary>
        /// <param name="cmdText">SQL Command Text to be executed</param>
        /// <returns>First column of first returned row as object</returns>
        public object ExecuteRead(string cmdText)
        {
            if (Open)
            {
                try
                {
                    switch (conType)
                    {
                        case 0:
                            using (SqlCommand sqlCmd = new SqlCommand(cmdText, mssqlConnection))
                            {
                                return sqlCmd.ExecuteScalar();
                            }

                        case 1:
                            using (MySqlCommand sqlCmd = new MySqlCommand(cmdText, mysqlConnection))
                            {
                                return sqlCmd.ExecuteScalar();
                            }
                    }
                }
                catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
            }

            return null;
        }

        /// <summary>
        /// Executes a database reader operation
        /// </summary>
        /// <param name="cmdText">SQL Command Text to be executed</param>
        /// <returns>Populated My/MS SQLDataReader object</returns>
        public object ExecuteReader(string cmdText)
        {
            if (Open)
            {
                try
                {
                    switch (conType)
                    {
                        case 0:
                            using (SqlCommand sqlCmd = new SqlCommand(cmdText, mssqlConnection))
                            {
                                return sqlCmd.ExecuteReader();
                            }

                        case 1:
                            using (MySqlCommand sqlCmd = new MySqlCommand(cmdText, mysqlConnection))
                            {
                                return sqlCmd.ExecuteReader();
                            }
                    }
                }
                catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
            }

            return null;
        }

        /// <summary>
        /// Executes a database write operation
        /// </summary>
        /// <param name="cmdText">SQL Command Text to be executed</param>
        /// <returns>Rows written</returns>
        public int ExecuteWrite(string cmdText)
        {
            if (Open)
            {
                try
                {
                    switch (conType)
                    {
                        case 0:
                            using (SqlCommand sqlCmd = new SqlCommand(cmdText, mssqlConnection))
                            {
                                return sqlCmd.ExecuteNonQuery();
                            }

                        case 1:
                            using (MySqlCommand sqlCmd = new MySqlCommand(cmdText, mysqlConnection))
                            {
                                return sqlCmd.ExecuteNonQuery();
                            }
                    }
                }
                catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
            }

            return 0;
        }

        #endregion

        #region Garbage Collection (GC)

        /// <summary>
		/// Free this object from memory
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this); // Garbage  Collector doesn't need to finalize this
		}

		/// <summary>
		/// Deconstructor (Finalize)
		/// </summary>
		~DBManager()
		{
			Dispose(false);
		}

		protected void Dispose(bool disposing)
		{
            if (mssqlConnection != null) { mssqlConnection.Dispose(); }
            if (mysqlConnection != null) { mysqlConnection.Dispose(); }
        }

        #endregion
    }
}