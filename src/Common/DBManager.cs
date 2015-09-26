//Written for Tartarus by iSmokeDrow
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

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
        readonly string dbConString;
        internal DbProviderFactory dbFactory;
        internal DbCommandBuilder dbBuilder;
        internal string dbParameterMarkerFormat;
		
		void dbProcess() { }

        public DBManager(int type, string conString)
        {
            dbConString = conString;

            switch (type)
            {
                case 0:
                    dbFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                    break;

                case 1:
                    dbFactory = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Database type isn't within range.");
            }

            if (dbFactory != null)
            {
                using (DbConnection dbCon = CreateConnection())
                {
                    dbCon.ConnectionString = dbConString;

                    dbBuilder = dbFactory.CreateCommandBuilder();

                    try
                    {
                        dbCon.Open();
                        using (DataTable tbl = dbCon.GetSchema(DbMetaDataCollectionNames.DataSourceInformation))
                        {
                            dbParameterMarkerFormat = tbl.Rows[0][DbMetaDataColumnNames.ParameterMarkerFormat] as string;
                        }
                        dbCon.Close();

                        if (!string.IsNullOrEmpty(dbParameterMarkerFormat)) { dbParameterMarkerFormat = "{0}"; }

                        ConsoleUtils.ShowDebug("DBManager Instance Initialized.");
                    }
                    catch (Exception ex) { ConsoleUtils.ShowError(ex.Message); }
                }
            }
        }

        public DbConnection CreateConnection()
        {
            DbConnection _dbCon = dbFactory.CreateConnection();
            _dbCon.ConnectionString = dbConString;
            return _dbCon;
        }

        public DbParameter CreateInParameter(DbCommand cmd, string name, DbType type, object value)
        {
            DbParameter dbParam = dbFactory.CreateParameter();
            dbParam.ParameterName = (string)typeof(DbCommandBuilder).InvokeMember("GetParameterName",
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.InvokeMethod |
                System.Reflection.BindingFlags.NonPublic, null, dbBuilder, new object[] {
                name });
            dbParam.DbType = type;
            dbParam.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParam);
            return dbParam;
        }

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
            dbFactory = null;
            dbBuilder = null;
            dbParameterMarkerFormat = null;
        }

        #endregion
    }
}